using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Hypersonic;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Uploader;
using TheMemorableMoments.Infrastructure.Repositories.Services;

namespace TheMemorableMoments.Infrastructure.Repositories.Uploader
{
    public class MediaQueueRepository : RepositoryBase, IMediaQueueRepository
    {
        private readonly IMediaFileHydrationService _mediaFileHydration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaQueueRepository"/> class.
        /// </summary>
        /// <param name="mediaFileHydration">The media file hydration.</param>
        public MediaQueueRepository(IMediaFileHydrationService mediaFileHydration)
        {
            _mediaFileHydration = mediaFileHydration;
        }

        /// <summary>
        /// Retrieves the queued by media id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public List<MediaQueue> RetrieveQueuedByMediaId(int mediaId)
        {
            List<DbParameter> parameters = new List<DbParameter>
                                               {
                                                   database.MakeParameter("@MediaId", mediaId)
                                               };

            return database.PopulateCollection("MediaQueue_RetrieveQueuedByMediaId", parameters, Populate);
        }

        /// <summary>
        /// Saves the exif.
        /// </summary>
        /// <param name="exifCollection">The exif collection.</param>
        public void SaveExif(DataTable exifCollection)
        {
            database.NonQuery("MediaQueue_InsertExif", new {exifCollection});
        }

        /// <summary>
        /// Retrieves the media by id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public Media RetrieveMediaById(int mediaId)
        {
            Media media = database.PopulateItem("MediaQueue_GetMediaByMediaId", new { mediaId }, database.AutoPopulate<Media>);
            _mediaFileHydration.Populate(new List<Media>{media});

            return media;
        }

        /// <summary>
        /// Inserts the specified queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        public void Insert(MediaQueue queue)
        {
            List<DbParameter> parameters = new List<DbParameter>
                                               {
                                                   database.MakeParameter("@MediaId", queue.MediaId),
                                                    database.MakeParameter("@MediaBytes", queue.MediaBytes),
                                                    database.MakeParameter("@Filename", queue.Filename),
                                               };

            database.NonQuery("MediaQueue_Insert", parameters);
        }

        /// <summary>
        /// Inserts the batch.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <param name="mediaId">The media id.</param>
        public void InsertBatchIdAndMediaId(Guid batchId, int mediaId)
        {
            database.NonQuery("MediaUploadBatch_Insert", new { batchId, mediaId});
        }

        /// <summary>
        /// Inserts the batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        public void InsertBatch(UploadBatch batch)
        {
            database.NonQuery("UploadBatch_Insert", batch);
        }

        /// <summary>
        /// Inserts the batch.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns></returns>
        public List<LoadedMedia> GetLoadedMedia(Guid batchId)
        {
            List<LoadedMedia> collection = database.PopulateCollection("MediaUpload_GetLoadedMedia", new { batchId }, database.AutoPopulate<LoadedMedia>);
            _mediaFileHydration.Populate(collection);

            return collection;
        }

        /// <summary>
        /// Deletes the specified media id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        public void Delete(int mediaId)
        {
            List<DbParameter> parameters = new List<DbParameter>
                                               {
                                                   database.MakeParameter("@MediaId", mediaId),
                                               };

            database.NonQuery("MediaQueue_Delete", parameters);
        }

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static MediaQueue Populate(INullableReader reader)
        {
            MediaQueue queue = new MediaQueue
                                   {
                                       Id = reader.GetInt64("MediaQueueId"),
                                       MediaId = reader.GetInt32("MediaId"),
                                       Filename = reader.GetString("Filename"),
                                       MediaBytes = (byte[]) reader.GetValue("MediaBytes")
                                   };
            return queue;
        }

        /// <summary>
        /// Gets the batch count.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns></returns>
        public int GetBatchCount(Guid batchId)
        {
            return database.PopulateItem("MediaUpload_GetBatchCount", new {batchId}, o => o.GetInt32("PhotoCount"));
        }

        /// <summary>
        /// Gets the upload batch.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns></returns>
        public UploadBatch GetUploadBatch(Guid batchId)
        {
            return database.PopulateItem("UploadBatch_GetById", new { batchId }, database.AutoPopulate<UploadBatch>);
        }

    }
}
