using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Albums;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class AlbumDecorator
    {
        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public Album Album { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumDecorator"/> class.
        /// </summary>
        /// <param name="album">The album.</param>
        public AlbumDecorator(Album album)
        {
            Album = album;
        }

        /// <summary>
        /// Gets or sets the album crumb.
        /// </summary>
        /// <value>The album crumb.</value>
        public string AlbumCrumb { get; set; }

        /// <summary>
        /// Gets or sets the album link.
        /// </summary>
        /// <value>The album link.</value>
        public string ChildAlbumLink { get; set; }

        /// <summary>
        /// Gets or sets the photo link.
        /// </summary>
        /// <value>The photo link.</value>
        public string PhotoLink { get; set; }

        /// <summary>
        /// Gets or sets the random image path.
        /// </summary>
        /// <value>The random image path.</value>
        public string RandomImagePath { get; set; }
    }
}