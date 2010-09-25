using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Albums;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class AlbumsView : BaseModel
    {

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        /// <value>The albums.</value>
        public List<Album> Albums { get; set; }
    }
}


