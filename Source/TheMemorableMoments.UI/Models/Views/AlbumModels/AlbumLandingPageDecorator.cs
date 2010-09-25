using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Albums;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class AlbumLandingPageDecorator
    {
        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public Album Album { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumLandingPageDecorator"/> class.
        /// </summary>
        /// <param name="album">The album.</param>
        public AlbumLandingPageDecorator(Album album)
        {
            Album = album;
        }

        /// <summary>
        /// Gets or sets the random image.
        /// </summary>
        /// <value>The random image.</value>
        public string RandomImage { get; set; }

    }
}