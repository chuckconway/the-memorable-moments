using System.Collections.Generic;
using System.IO;
using Chucksoft.Storage;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;


namespace TheMemorableMoments.Domain.Services
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaFilenameService _mediaFilenameService;
        private readonly IStorage _fileService;

        public MediaFileService(IMediaRepository mediaRepository, IMediaFilenameService mediaFilenameService, IStorage fileService)
        {
            _mediaRepository = mediaRepository;
            _fileService = fileService;
            _mediaFilenameService = mediaFilenameService;
        }

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public List<MediaFile> CreateMediaFiles(string fileName)
        {
            List<MediaFile> mediaFiles = new List<MediaFile>();

            //Save Thumbnail
            MediaFile thumbNail = QueueFile(fileName, PhotoType.Thumbnail);
            mediaFiles.Add(thumbNail);

            //Save web Version
            MediaFile web = QueueFile(fileName,PhotoType.Websize);
            mediaFiles.Add(web);

            return mediaFiles;
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        private MediaFile QueueFile(string fileName, PhotoType mediaType)
        {
            MediaFile mediaFile = new MediaFile
                                      {
                                          OriginalFileName = fileName,
                                          FileExtension = Path.GetExtension(fileName),
                                          FilePath = _mediaFilenameService.GetFilename(fileName, mediaType),
                                          PhotoType = mediaType
                                      };

            mediaFile.MediaFormat = (mediaFile.FileExtension.Contains("fla") ? MediaFormat.Video : MediaFormat.Photo);
            return mediaFile;
        }

        /// <summary>
        /// Deletes the media.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="mediaId">The media id.</param>
        public void DeleteMedia(User user, int mediaId)
        {
            Media media = _mediaRepository.RetrieveByPrimaryKeyAndUserId(mediaId, user.Id);
            media.MediaFiles.ForEach(o=> _fileService.DeleteFile(user.Username, o.FilePath));
            _mediaRepository.Delete(mediaId, user.Id);
        }
    }
}
