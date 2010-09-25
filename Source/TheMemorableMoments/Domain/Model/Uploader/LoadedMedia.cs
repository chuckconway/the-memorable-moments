using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Model.Uploader
{
    public class LoadedMedia : Media
    {
        /// <summary>
        /// Gets or sets the total upload in batch.
        /// </summary>
        /// <value>The total upload in batch.</value>
        public int TotalUploadInBatch { get; set; }
    }
}
