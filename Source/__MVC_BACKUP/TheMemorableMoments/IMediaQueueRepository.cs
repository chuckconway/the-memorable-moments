using System;
using System.Collections.Generic;
using System.Data;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Uploader;

namespace TheMemorableMoments
{
    public interface IMediaQueueRepository
    {
        /// <summary>
        /// Retrieves the queued by media id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        List<MediaQueue> RetrieveQueuedByMediaId(int mediaId);

        /// <summary>
        /// Inserts the specified queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        void Insert(MediaQueue queue);

        /// <summary>
        /// Deletes the specified media id.
        /// </summary>
        /// <param name="id">The id.</param>
        void Delete(int id);

        /// <summary>
        /// Inserts the batch id and media id.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <param name="mediaId">The media id.</param>
        void InsertBatchIdAndMediaId(Guid batchId, int mediaId);

        /// <summary>
        /// Inserts the batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        void InsertBatch(UploadBatch batch);

        /// <summary>
        /// Saves the exif.
        /// </summary>
        /// <param name="exifCollection">The exif collection.</param>
        void SaveExif(DataTable exifCollection);

        /// <summary>
        /// Gets the upload batch.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns></returns>
        UploadBatch GetUploadBatch(Guid batchId);

        /// <summary>
        /// Retrieves the media by id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        Media RetrieveMediaById(int mediaId);

        /// <summary>
        /// Gets the loaded media.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns></returns>
        List<LoadedMedia> GetLoadedMedia(Guid batchId);

        /// <summary>
        /// Gets the batch count.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns></returns>
        int GetBatchCount(Guid batchId);
    }
}