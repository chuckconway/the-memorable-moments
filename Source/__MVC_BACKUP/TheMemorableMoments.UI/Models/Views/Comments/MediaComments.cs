using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views.Comments
{
    public class MediaComments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaComments"/> class.
        /// </summary>
        public MediaComments(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaComments"/> class.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="comment">The comment.</param>
        public MediaComments(Media media, Comment comment)
        {
            Media = media;
            Comment = comment;
        }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public Media Media { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        public Comment Comment { get; set; }
    }
}