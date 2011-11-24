using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class RemovePhotosFromAlbumView : BaseModel
    {
        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        /// <value>The photos.</value>
        public List<Media> Photos { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }
    }
}