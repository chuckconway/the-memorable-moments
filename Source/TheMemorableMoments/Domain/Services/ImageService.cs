using System;
using System.Collections.Generic;
using Chucksoft.Core.Drawing;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.FileService;

namespace TheMemorableMoments.Domain.Services
{
    public class ImageService : IImageService
    {
        private readonly IFileService _fileService;
        private IMediaFileRepository _mediaFileRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        /// <param name="mediaFileRepository">The media file repository.</param>
        public ImageService(IFileService fileService, IMediaFileRepository mediaFileRepository)
        {
            _fileService = fileService;
            _mediaFileRepository = mediaFileRepository;
        }

        /// <summary>
        /// Rotates the specified media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="user">The user.</param>
        /// <param name="rotate">The rotate.</param>
        private void Rotate(IEnumerable<MediaFile> media, User user, Func<byte[],byte[]> rotate)
        {
            foreach (MediaFile file in media)
            {
                byte[] bytes = GetFileBytes(file, user);
                bytes = rotate(bytes);
                string contentType = GetContentType(file);
                _fileService.AddFile(user.Username, file.FilePath, contentType, bytes);
                _mediaFileRepository.UpdateDimension(file.FileId, file.Height, file.Width);
            }
        }

        /// <summary>
        /// Gets the file bytes.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public byte[] GetFileBytes(MediaFile file, User user)
        {
            return _fileService.GetFile(user.Username, file.FilePath);
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public string GetContentType(MediaFile file)
        {
            string contentType = "image/" + file.FileExtension.Replace(".", string.Empty);
            return contentType;
        }

        /// <summary>
        /// Rotates the left.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="user">The user.</param>
        public void RotateLeft(List<MediaFile> media, User user)
        {
            Rotate(media, user, ImageResizer.Rotate90DegreesLeft);
        }

        /// <summary>
        /// Rotates the right.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="user">The user.</param>
        public void RotateRight(List<MediaFile> media, User user)
        {
            Rotate(media, user, ImageResizer.Rotate90DegreesRight);
        }
    }
}
