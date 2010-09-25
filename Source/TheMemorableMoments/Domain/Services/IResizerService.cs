using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public interface IResizerService
    {
        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        byte[] ResizeImage(byte[] image, PhotoType type);
    }
}