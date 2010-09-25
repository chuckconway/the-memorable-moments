using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.UI.Models.Views
{
    public class TagsIndexView : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        /// <value>The name of the tag.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the set.
        /// </summary>
        /// <value>The set.</value>
        public string Set { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public List<Media> Media { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public Tag Tag {get; set; }

        /// <summary>
        /// Gets or sets the related tags.
        /// </summary>
        /// <value>The related tags.</value>
        public string RelatedTags { get; set; }
    }
}
