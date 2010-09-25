using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments
{
    public interface IImageService
    {
        /// <summary>
        /// Rotates the left.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="user">The user.</param>
        void RotateLeft(List<MediaFile> media, User user);

        /// <summary>
        /// Rotates the right.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="user">The user.</param>
        void RotateRight(List<MediaFile> media, User user);

        /// <summary>
        /// Gets the file bytes.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        byte[] GetFileBytes(MediaFile file, User user);

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        string GetContentType(MediaFile file);
    }
}