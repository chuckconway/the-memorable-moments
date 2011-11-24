using System;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public interface IMediaBatchService
    {
        /// <summary>
        /// Updates the details.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="batchId">The batch id.</param>
        void UpdateDetails(Media media, Guid batchId);
    }
}