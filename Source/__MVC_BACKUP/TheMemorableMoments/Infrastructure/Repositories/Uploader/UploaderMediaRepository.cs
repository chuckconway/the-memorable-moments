using System.Collections.Generic;
using System.Data.Common;
using Chucksoft.Core.Extensions;
using Hypersonic;
using TheMemorableMoments.Domain.Model.Uploader;

namespace TheMemorableMoments.Infrastructure.Repositories.Uploader
{
   public class UploaderMediaRepository :RepositoryBase, IUploaderMediaRepository
   {
        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
       public UploaderMedia RetrieveByPrimaryKey(int key)
       {
           List<DbParameter> parameters = new List<DbParameter>
                                               {
                                                   database.MakeParameter("@MediaId", key)
                                               };

           return database.PopulateItem("UploaderMedia_SelectByPrimaryKey", parameters, Populate);
       }

       /// <summary>
       /// Updates the media status.
       /// </summary>
       /// <param name="key">The key.</param>
       /// <param name="uploadstatus">The uploadstatus.</param>
       public void UpdateMediaStatus(int key, MediaUploadState uploadstatus)
       {
           database.NonQuery("UploaderMedia_UpdateStatus",new  {mediaId = key, status = uploadstatus.ToString()});
       }

       /// <summary>
       /// Gets the queued items set items to pending.
       /// </summary>
       /// <returns></returns>
       public List<UploaderMedia> GetQueuedItemsAndSetItemsToPending()
       {
           return database.PopulateCollection("UploaderMedia_SelectAllAndSetUploadStatusToPending", Populate);
       }

       /// <summary>
       /// Populates the specified reader.
       /// </summary>
       /// <param name="reader">The reader.</param>
       /// <returns></returns>
       private static UploaderMedia Populate(INullableReader reader)
       {
           UploaderMedia media = new UploaderMedia
                                     {
                                         Id = reader.GetInt32("MediaId"),
                                         UploadStatus = reader.GetString("UploadStatus").ParseEnum<MediaUploadState>(),
                                         UserId = reader.GetInt32("UserId")
                                     };

           return media;
       }
    }

    
    public enum MediaUploadState
    {
        Completed,
        Pending,
        Queued,
        Uploading
    }
}
