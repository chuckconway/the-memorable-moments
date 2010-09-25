namespace TheMemorableMoments.UI.Models.Views.Photos
{
    public class ManagePhotoPostView
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Submit { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
        public int[] MediaId { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the media status.
        /// </summary>
        /// <value>The media status.</value>
        public string MediaStatus { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public string CP { get; set; }
    }
}