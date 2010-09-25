namespace TheMemorableMoments.UI.Models.Views
{
    public abstract class PhotoDetailsBase
    {
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>The zoom.</value>
        public byte? Zoom { get; set; }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>The zoom.</value>
        public string MapTypeId { get; set; }
        
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? SelectedYear { get; set; }

        /// <summary>
        /// Gets or sets the selected month.
        /// </summary>
        /// <value>The selected month.</value>
        public int? SelectedMonth { get; set; }

        /// <summary>
        /// Gets or sets the selected day.
        /// </summary>
        /// <value>The selected day.</value>
        public int? SelectedDay { get; set; }

        /// <summary>
        /// Gets or sets the selected albums.
        /// </summary>
        /// <value>The selected albums.</value>
        public string SelectedAlbums { get; set; }

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

    }
}