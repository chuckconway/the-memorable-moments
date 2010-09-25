using System;
using System.Collections.Generic;
using System.Drawing;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Uploader;
using TheMemorableMoments.Domain.Services;

namespace TheMemorableMoments.Uploader
{
    public class PhotoSizer
    {
        private readonly UploaderMedia _uploaderMedia;
        private readonly IResizerService _resizerService = DependencyInjection.Resolve<IResizerService>();
        private readonly IMediaFileService _mediaFileService = DependencyInjection.Resolve<IMediaFileService>();
        private readonly IMediaFileRepository _mediaFileRepository = DependencyInjection.Resolve<IMediaFileRepository>();
        private readonly IQueueFileService _queueFileService = DependencyInjection.Resolve<IQueueFileService>();
        readonly IMediaQueueRepository _mediaQueueRepository = DependencyInjection.Resolve<IMediaQueueRepository>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoSizer"/> class.
        /// </summary>
        /// <param name="uploaderMedia">The uploader media.</param>
        public PhotoSizer(UploaderMedia uploaderMedia )
        {
            _uploaderMedia = uploaderMedia;
        }

        /// <summary>
        /// Images the resizer.
        /// </summary>
        public void ResizeTheImages()
        {
            List<MediaFile> mediaFiles = CreateAndInsertMediaFiles();
            MediaQueue mediaQueue = _mediaQueueRepository.RetrieveQueuedByMediaId(_uploaderMedia.Id)[0];

            foreach (MediaFile file in mediaFiles)
            {
                byte[] bytes = _resizerService.ResizeImage(mediaQueue.MediaBytes, file.PhotoType);

                using (Bitmap bitmap = _queueFileService.GetBitmap(bytes))
                {
                    file.Size = bytes.LongLength;
                    file.Width = bitmap.Width;
                    file.Height = bitmap.Height;
                }
                
                _queueFileService.AddFileToQueue(bytes, _uploaderMedia.Id, file);
            }

            _mediaFileRepository.Save(mediaFiles);
        }

        /// <summary>
        /// Creates the and insert media files.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        private List<MediaFile> CreateAndInsertMediaFiles()
        {
            Media media = _mediaQueueRepository.RetrieveMediaById(_uploaderMedia.Id);

            if(media == null)
            {
                throw  new ArgumentNullException("media", "Media is null expected value.");
            }
            MediaFile mediaFile = media.GetImageByPhotoType(PhotoType.Original);
            List<MediaFile> mediaFiles = _mediaFileService.CreateMediaFiles(mediaFile.OriginalFileName);
            mediaFiles.ForEach(x => x.MediaId = media.MediaId);
            return mediaFiles;
        }
    }
}
