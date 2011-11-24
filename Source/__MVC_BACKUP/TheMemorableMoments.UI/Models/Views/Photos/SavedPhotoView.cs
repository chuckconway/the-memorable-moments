namespace TheMemorableMoments.UI.Models.Views.Photos
{
    public class SavedPhotoView : BaseModel
    {
        /// <summary>
        /// Gets or sets the photo title.
        /// </summary>
        /// <value>The photo title.</value>
        public string PhotoTitle { get; set; }

        /// <summary>
        /// Gets or sets the story.
        /// </summary>
        /// <value>The story.</value>
        public string Story { get; set; }

        /// <summary>
        /// Gets or sets the media status.
        /// </summary>
        /// <value>The media status.</value>
        public string MediaStatus { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>The month.</value>
        public int? SelectedMonth { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public int? SelectedDay { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? Year { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public string Tags { get; set; }
    }
}