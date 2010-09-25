using System.Collections.Generic;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class ManageAlbumsView : SidebarView
    {

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public string AlbumName { get; set; }


        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        /// <value>The album id.</value>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the top level albums.
        /// </summary>
        /// <value>The top level albums.</value>
        public List<AlbumLandingPageDecorator>  TopLevelAlbums { get; set; }
    }
}


