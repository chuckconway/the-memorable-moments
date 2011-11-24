using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public interface IMediaFileService
    {
        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        List<MediaFile> CreateMediaFiles(string fileName);

        /// <summary>
        /// Deletes the media.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="mediaId">The media id.</param>
        void DeleteMedia(User user, int mediaId);
    }
}