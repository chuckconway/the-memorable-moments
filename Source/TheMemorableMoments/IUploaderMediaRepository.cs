using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Uploader;
using TheMemorableMoments.Infrastructure.Repositories.Uploader;

namespace TheMemorableMoments
{
    public interface IUploaderMediaRepository
    {
        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        UploaderMedia RetrieveByPrimaryKey(int key);

        /// <summary>
        /// Updates the media status.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="status">The status.</param>
        void UpdateMediaStatus(int key, MediaUploadState status);

        /// <summary>
        /// Gets the queued items and set items to pending.
        /// </summary>
        /// <returns></returns>
        List<UploaderMedia> GetQueuedItemsAndSetItemsToPending();
    }
}