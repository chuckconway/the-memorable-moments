using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views
{
    public class DisplayView : BaseModel 
    {

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public List<Media> Media { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        /// <value>The name of the tag.</value>
        public string TagName { get; set; }

   
    }
}
