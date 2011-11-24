using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.Domain.Model.MediaClasses
{
    public class MediaGroupedByTag :Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaGroupedByTag"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public MediaGroupedByTag(string tag): base(tag){}

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public List<Media> Media { get; set; }
    }
}
