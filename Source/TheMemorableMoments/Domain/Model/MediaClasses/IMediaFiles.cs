using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Albums;

namespace TheMemorableMoments.Domain.Model.MediaClasses
{
    public interface IMediaFiles
    {
        /// <summary>
        /// Gets or sets the media files.
        /// </summary>
        /// <value>The media files.</value>
        List<MediaFile> MediaFiles { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
        int MediaId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        Location Location { get; set; }

        /// <summary>
        /// Gets or sets the belongs to albums.
        /// </summary>
        /// <value>The belongs to albums.</value>
        List<BelongsToAlbum> BelongsToAlbums { get; set; }
    }
}