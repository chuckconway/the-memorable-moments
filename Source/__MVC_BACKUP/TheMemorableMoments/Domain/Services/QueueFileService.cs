using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Uploader;
using ImageConverter = Chucksoft.Core.Drawing.ImageConverter;

namespace TheMemorableMoments.Domain.Services
{
    public class QueueFileService : IQueueFileService
    {

        private readonly IMediaQueueRepository _mediaQueueRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaBatchService _mediaBatchService;
        private readonly IMediaFilenameService _mediaFilenameService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueFileService"/> class.
        /// </summary>
        /// <param name="mediaQueueRepository">The media queue repository.</param>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="mediaBatchService">The media batch service.</param>
        /// <param name="mediaFilenameService">The media filename service.</param>
        public QueueFileService(IMediaQueueRepository mediaQueueRepository,
            IMediaRepository mediaRepository,
            IMediaBatchService mediaBatchService, IMediaFilenameService mediaFilenameService)
        {
            _mediaQueueRepository = mediaQueueRepository;
            _mediaFilenameService = mediaFilenameService;
            _mediaBatchService = mediaBatchService;
            _mediaRepository = mediaRepository;
        }


        /// <summary>
        /// Adds the file to queue.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="mediaId">The media id.</param>
        /// <param name="file">The file.</param>
        public void AddFileToQueue(byte[] bytes, int mediaId, MediaFile file)
        {
            MediaQueue mediaQueue = new MediaQueue { MediaId = mediaId, Filename = file.FilePath, MediaBytes = bytes };
            _mediaQueueRepository.Insert(mediaQueue);
        }

        /// <summary>
        /// Extracts the exif save.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="mediaId">The media id.</param>
        private DateTime ExtractExifSave(Stream stream, int mediaId)
        {
            DateTime date = DateTime.MinValue;

            using (stream)
            {
                Image image = Image.FromStream(stream);
                PropertyItem[] items = image.PropertyItems;

                if (items.Length > 0)
                {
                    DataTable table = GetTable();

                    foreach (PropertyItem propertyItem in items)
                    {
                        //The below codes output funky strings that won't go into the database.
                        string[] ignore = {"501b", "5091", "5090", "927c"};
                        string id = propertyItem.Id.ToString("x");
                        string value = Encoding.UTF8.GetString(propertyItem.Value);

                        if(!ignore.Contains(id))
                        {
                            DataRow dataRow = table.NewRow();
                            dataRow["MediaId"] = mediaId;
                            dataRow["Key"] = propertyItem.Id.ToString("x");
                            dataRow["Type"] = propertyItem.Type;
                            dataRow["Value"] = value;
                            table.Rows.Add(dataRow);
                        }

                        date = GetDate(id, value, date);
                    }

                    try
                    {
                        _mediaQueueRepository.SaveExif(table);
                    }
                    catch //This needs to be logged... TODO:Log this! Maybe we can tap into ELMAH
                    {
                        
                    }
                }
            }

            return date;
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private static DateTime GetDate(string id, string value, DateTime date)
        {
            if (id == "9003" && !string.IsNullOrEmpty(value))
            {
                try
                {
                    DateTime? taken = DateTaken(value);
                    date = (taken.HasValue ? taken.GetValueOrDefault() : DateTime.MinValue);
                }
                catch // can not fixed f'ed up dates
                {

                }
            }
            return date;
        }

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <returns></returns>
        private static DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MediaId", typeof (int));
            table.Columns.Add("Key", typeof (string));
            table.Columns.Add("Type", typeof(int));
            table.Columns.Add("Value", typeof (string));
            return table;
        }

        /// <summary>
        /// Returns the EXIF Image Data of the Date Taken.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Date Taken or Null if Unavailable</returns>
        public static DateTime? DateTaken(string value)
        {
            string dateTakenTag = value;
            string[] parts = dateTakenTag.Split(':', ' ');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);
            int hour = int.Parse(parts[3]);
            int minute = int.Parse(parts[4]);
            int second = int.Parse(parts[5]);

            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Queues the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="batchId">The batch id.</param>
        /// <param name="orginal">The orginal bytes.</param>
        /// <param name="stream">The stream.</param>
        public void QueueFile(string fileName, int userId, Guid batchId, byte[] orginal, Stream stream)
        {
            MediaFile mediaFile = GetMediaFile(fileName, orginal);

            using (Bitmap bitmap = GetBitmap(orginal))
            {
                mediaFile.Width = bitmap.Width;
                mediaFile.Height = bitmap.Height;
            }

            Media media = new Media { MediaFiles = new List<MediaFile> { mediaFile }, Owner = new Owner { UserId = userId } };

            int mediaId = _mediaRepository.Save(media);
            DateTime date = ExtractExifSave(stream, mediaId);

            if (date != DateTime.MinValue)
            {
                media.Year = date.Year;
                media.Month = date.Month;
                media.Day = date.Day;
            }

            media.MediaId = mediaId;
            media.MediaFiles.ForEach(o => AddFileToQueue(orginal, mediaId, o));
            _mediaBatchService.UpdateDetails(media, batchId);

        }

        /// <summary>
        /// Gets the media file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="orginal">The orginal.</param>
        /// <returns></returns>
        private MediaFile GetMediaFile(string fileName, byte[] orginal)
        {
            return new MediaFile
                       {
                           OriginalFileName = fileName,
                           FileExtension = Path.GetExtension(fileName),
                           FilePath = _mediaFilenameService.GetFilename(fileName, PhotoType.Original),
                           PhotoType = PhotoType.Original,
                           MediaFormat = MediaFormat.Photo,
                           Size = orginal.LongLength,
                       };
        }

        /// <summary>
        /// Gets the bitmap.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public Bitmap GetBitmap(byte[] bytes)
        {
            ImageConverter imageConverter = new ImageConverter();
            return imageConverter.ConvertByteArrayToBitmap(bytes);
        }
    }
}
