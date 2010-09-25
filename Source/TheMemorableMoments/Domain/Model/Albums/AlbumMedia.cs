using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMemorableMoments.Domain.Model.Albums
{
    public class BelongsToAlbum
    {
        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        /// <value>The album id.</value>
        public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
        public int MediaId { get; set; }

        /// <summary>
        /// Gets or sets the name of the album.
        /// </summary>
        /// <value>The name of the album.</value>
        public string AlbumName { get; set; }
    }
}
