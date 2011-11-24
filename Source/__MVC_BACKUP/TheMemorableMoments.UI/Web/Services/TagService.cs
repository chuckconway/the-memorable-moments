using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.UI.Web.Services
{
    public class TagService : ITagService
    {  
        /// <summary>
        /// Hyperlinks the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public string HyperlinkTheTags(string tags, string username)
        {
            tags = tags.Trim();
            string[] tagArray = tags.Split(',', ' ');
            return RenderTags(tagArray, username);
        }

        /// <summary>
        /// Hyperlinks the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public string HyperlinkTheTags(List<Tag> tags, string username)
        {
            string[] tagArray = GetTags(tags);
            return RenderTags(tagArray, username);
        }

        /// <summary>
        /// Hyperlinks the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public string HyperlinkTheAdminTags(string tags, string username, int mediaId)
        {
            tags = tags.Trim();
            string[] tagArray = tags.Split(',', ' ');
            return RenderAdminTags(tagArray, username, mediaId);
        }

        /// <summary>
        /// Hyperlinks the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public string HyperlinkTheAdminTags(List<Tag> tags, string username, int mediaId)
        {
            string[] tagArray = GetTags(tags);
            return RenderAdminTags(tagArray, username, mediaId);
        }

        /// <summary>
        /// Renders the tags.
        /// </summary>
        /// <param name="tagArray">The tag array.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        private static string RenderTags(IList<string> tagArray, string username)
        {
            StringBuilder builder = new StringBuilder();
            const string linkFormat = ("<a href=\"/{0}/tagged/{1}\" title=\"view photos tagged as '{1}'\">{1}</a>{2} ");

            for (int i = 0; i < tagArray.Count; i++)
            {
                string comma = (i != tagArray.Count - 1 ? ", " : string.Empty);

                if (!string.IsNullOrEmpty(tagArray[i]))
                {
                    builder.AppendLine(string.Format(linkFormat, username, tagArray[i], comma));
                }
            }

            string val = builder.ToString();
            val = (string.IsNullOrEmpty(val) ? null : val);
            return val;
        }

        /// <summary>
        /// Renders the tags.
        /// </summary>
        /// <param name="tagArray">The tag array.</param>
        /// <param name="username">The username.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        private static string RenderAdminTags(IList<string> tagArray, string username, int mediaId)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < tagArray.Count; i++)
            {

                if (!string.IsNullOrEmpty(tagArray[i]))
                {
                    string link = RenderAdminTag(tagArray[i],username, mediaId);
                    builder.AppendLine(link);
                }
            }

            string val = builder.ToString();
            val = (string.IsNullOrEmpty(val) ? null : val);
            return val;
        }

        /// <summary>
        /// Renders the tags.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="username">The username.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        private static string RenderAdminTag(string tag, string username, int mediaId)
        {
            string link = string.Format("<span class=\"taggroup\" ><a href=\"/{0}/tagged/{1}\" title=\"view photos tagged as '{1}'\">{1}</a> <a class=\"removetag\" title=\"remove '{1}'\"  href=\"/{0}/tags/remove/{2}?tag={1}\"></a>   </span>", username, tag, mediaId);
            return link;
        }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        private static string[] GetTags(IList<Tag> tags)
        {
            string[] stringTags = new string[tags.Count];

            for (int index = 0; index < tags.Count; index++)
            {
                stringTags[index] = tags[index].TagText;
            }

            return stringTags;
        }
    }
}