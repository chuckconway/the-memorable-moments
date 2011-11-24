using System;

namespace TheMemorableMoments.Domain.Model.Uploader
{
   public class MediaBatch
   {
       /// <summary>
       /// Gets or sets the media id.
       /// </summary>
       /// <value>The media id.</value>
       public int MediaId { get; set; }

       /// <summary>
       /// Gets or sets the batch id.
       /// </summary>
       /// <value>The batch id.</value>
       public Guid BatchId { get; set; }
   }
}
