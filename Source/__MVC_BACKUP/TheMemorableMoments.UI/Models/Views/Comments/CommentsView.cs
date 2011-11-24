using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views.Photos;

namespace TheMemorableMoments.UI.Models.Views.Comments
{
    public class CommentsView : PhotoView
    {

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }


        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public Media Media { get; set; }


        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public List<Comment> Comments { get; set; }

    }
}


