using System.Web;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views.Photos
{
    public class PhotoView : BaseModel
    {
        /// <summary>
        /// Gets or sets the media keys.
        /// </summary>
        /// <value>The media keys.</value>
        public string MediaKeys { get; set; }

        /// <summary>
        /// Gets or sets the back link.
        /// </summary>
        /// <value>The back link.</value>
        public string BackLink { get; set; }

        /// <summary>
        /// Gets or sets the set id.
        /// </summary>
        /// <value>The set id.</value>
        public string SetId { get; set; }
        
    }
}