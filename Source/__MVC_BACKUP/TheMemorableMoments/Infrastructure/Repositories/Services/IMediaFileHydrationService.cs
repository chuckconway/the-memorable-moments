using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Infrastructure.Repositories.Services
{
    public interface IMediaFileHydrationService
    {
        /// <summary>
        /// Populates the files.
        /// </summary>
        /// <param name="media">The media.</param>
        void Populate<T>(List<T> media) where T : IMediaFiles;
    }
}