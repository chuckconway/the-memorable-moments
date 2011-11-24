using System.Collections.Generic;
using Hypersonic.Attributes;
using TheMemorableMoments.Domain.Model.MediaClasses;


namespace TheMemorableMoments.Domain.Model.Albums
{
	public class Album 
	{
        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        /// <value>The album id.</value>
		public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
		public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
		public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
		public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
	    public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the default media id.
        /// </summary>
        /// <value>The default media id.</value>
        [IgnoreParameter]
        public int? CoverMediaId { get; set; }

        /// <summary>
        /// Gets or sets the child album count.
        /// </summary>
        /// <value>The child album count.</value>
        [IgnoreParameter]
        public int ChildAlbumCount { get; set; }

        /// <summary>
        /// Gets or sets the photo count.
        /// </summary>
        /// <value>The photo count.</value>
        [IgnoreParameter]
        public int PhotoCount { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        [IgnoreParameter]
        public List<Media> Media { get; set; }

	}
}
