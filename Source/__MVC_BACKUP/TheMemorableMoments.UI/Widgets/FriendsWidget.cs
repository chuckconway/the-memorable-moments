using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views;

namespace TheMemorableMoments.UI.Widgets
{
    public static class FriendsWidget
    {
        /// <summary>
        /// Renders the specified albums.
        /// </summary>
        /// <param name="friends">The friends.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static string Render(IList<Friend> friends, string username)
        {
            StringBuilder builder = new StringBuilder();
            string friendHtml = string.Empty;

            if (friends == null) { throw new System.ArgumentNullException("friends", "Parameter 'friends' is null, value expected"); }

            if (friends.Count > 0)
            {
                bool hasFriendImages = GetHasFriendWithImages(friends);

                if (hasFriendImages)
                {
                    friendHtml = GetFriendHtml(friends, builder, username);
                }
            }

            return friendHtml; 
        }


        /// <summary>
        /// Gets the friend HTML.
        /// </summary>
        /// <param name="friends">The friends.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        private static string GetFriendHtml(IEnumerable<Friend> friends, StringBuilder builder, string username)
        {
            builder.AppendLine(string.Format( "<h3><a href=\"/{0}/friends\" >friends</a></h3>", username));
            builder.AppendLine("<ul class=\"thumbs\">");

            foreach (Friend t in friends)
            {
                builder.AppendLine(PhotoHtmlHelper.GetFriendImageThumbnailForUserHomepage(t));
            }

            builder.AppendLine("</ul>");
            builder.AppendLine("<div style=\"clear:both;\" ></div>");

            return builder.ToString();
        }

        /// <summary>
        /// Gets the has friend with images.
        /// </summary>
        /// <param name="friends">The friends.</param>
        /// <returns></returns>
        private static bool GetHasFriendWithImages(IEnumerable<Friend> friends)
        {
            bool hasFriendImages = friends.Any(i => i.Media != null);
            return hasFriendImages;
        }
    }
}
          
