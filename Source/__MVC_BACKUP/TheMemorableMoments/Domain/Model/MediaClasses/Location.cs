using Hypersonic.Attributes;

namespace TheMemorableMoments.Domain.Model.MediaClasses
{
    public class Location
    {
        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        /// <value>The name of the location.</value>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>The zoom.</value>
        public byte Zoom { get; set; }

        /// <summary>
        /// Gets or sets the map type id.
        /// </summary>
        /// <value>The map type id.</value>
        public string MapTypeId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [IgnoreParameter]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
        [IgnoreParameter]
        public int MediaId { get; set; }
    }
}
