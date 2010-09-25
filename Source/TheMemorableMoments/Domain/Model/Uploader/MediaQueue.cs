using System;
using Chucksoft.Core;

namespace TheMemorableMoments.Domain.Model.Uploader
{
   public class MediaQueue : IId<long>
   {
       /// <summary>
       /// Gets or sets the id.
       /// </summary>
       /// <value>The id.</value>
       public long Id { get; set; }

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

       /// <summary>
       /// Gets or sets the media bytes.
       /// </summary>
       /// <value>The media bytes.</value>
       public byte[] MediaBytes { get; set; }

       /// <summary>
       /// Gets or sets the filename.
       /// </summary>
       /// <value>The filename.</value>
       public string Filename { get; set; }
    }
}
