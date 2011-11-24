using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public interface IMediaFilenameService
    {
        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="photoType">Type of the photo.</param>
        /// <returns></returns>
        string GetFilename(string filename, PhotoType photoType);
    }
}