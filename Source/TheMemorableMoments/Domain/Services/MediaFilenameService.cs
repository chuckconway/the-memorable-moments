using System;
using System.IO;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public class MediaFilenameService : IMediaFilenameService
    {
        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="photoType">Type of the photo.</param>
        /// <returns></returns>
        public string GetFilename(string filename, PhotoType photoType)
        {
            string name = string.Format("{0}_{1}_{2}{3}", Path.GetFileNameWithoutExtension(filename), photoType, DateTime.UtcNow.Ticks, Path.GetExtension(filename));
            return name;
        }

    }
}
