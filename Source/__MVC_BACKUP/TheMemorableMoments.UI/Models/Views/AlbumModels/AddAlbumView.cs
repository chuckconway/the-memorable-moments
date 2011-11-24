using TheMemorableMoments.UI.Web.Controls;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class AddAlbumView : ManageViewBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name  { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public IAlbumTabText Link { get; set; }

        /// <summary>
        /// Gets or sets the album crumbs.
        /// </summary>
        /// <value>The album crumbs.</value>
        public string AlbumCrumbs { get; set; }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>The header text.</value>
        public string HeaderText { get; set; }

    }
}


