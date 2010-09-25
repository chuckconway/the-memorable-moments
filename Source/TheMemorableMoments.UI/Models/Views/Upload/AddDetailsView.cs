namespace TheMemorableMoments.UI.Models.Views.Upload
{
    public class AddDetailsView : BaseModel
    {
        /// <summary>
        /// Gets or sets the media keys.
        /// </summary>
        /// <value>The media keys.</value>
        public string MediaKeys { get; set; }

        /// <summary>
        /// Gets or sets the persistent key.
        /// </summary>
        /// <value>The persistent key.</value>
        public string PersistentKey { get; set; }

        /// <summary>
        /// Gets or sets the batch id.
        /// </summary>
        /// <value>The batch id.</value>
        public string BatchId { get; set; } 

        /// <summary>
        /// Gets or sets the photo count.
        /// </summary>
        /// <value>The photo count.</value>
        public int PhotoCount { get; set; }
    }
}
