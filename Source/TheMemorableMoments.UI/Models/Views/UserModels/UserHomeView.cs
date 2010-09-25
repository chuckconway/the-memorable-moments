using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views.UserModels
{
    public class UserHomeView : SidebarView
    {
        /// <summary>
        /// Gets or sets the meida.
        /// </summary>
        /// <value>The meida.</value>
        public IList<Media> Media { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the set id.
        /// </summary>
        /// <value>The set id.</value>
        public string Set { get; set; }

        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public  string GetImageLink(string set, Media media)
        {
            return PhotoHtmlHelper.GetImageLink(set, media);
        }
    }
}


