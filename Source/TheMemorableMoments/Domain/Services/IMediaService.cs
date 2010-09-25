using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public interface IMediaService
    {
        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="fullsizeBytes">The fullsize bytes.</param>
        /// <param name="rootSaveDirectory">The root save directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        List<MediaFile> SaveImages(byte[] fullsizeBytes, string rootSaveDirectory, string fileName);

        /// <summary>
        /// Rotates the left.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="container">The container.</param>
        void RotateLeft(List<MediaFile> media, string container);

        /// <summary>
        /// Rotates the right.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="container">The container.</param>
        void RotateRight(List<MediaFile> media, string container);
    }
}