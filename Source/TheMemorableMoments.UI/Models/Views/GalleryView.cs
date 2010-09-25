using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views
{
    public class GalleryView : BaseModel
    {
        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public List<Media> Media { get; set; }
        
        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        /// <value>The pagination.</value>
        public string Pagination { get; set; }


        /// <summary>
        /// Gets or sets the photo count.
        /// </summary>
        /// <value>The photo count.</value>
        public int PhotoCount { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        public string ReturnUrl { get; set; }
    }
}
