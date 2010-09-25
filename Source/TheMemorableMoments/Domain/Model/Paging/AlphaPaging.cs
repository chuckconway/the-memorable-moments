using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Model.Paging
{
    /// <summary>
    /// 
    /// </summary>
    public class AlphaPaging
    {
        /// <summary>
        /// Gets or sets the tag count.
        /// </summary>
        /// <value>The tag count.</value>
        public int TagCount { get; set; }

        /// <summary>
        /// Gets or sets the media grouped by tags.
        /// </summary>
        /// <value>The media grouped by tags.</value>
        public List<MediaGroupedByTag> MediaGroupedByTags { get; set; }
    }
}
