using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.UI.Web.Services
{
    public interface ITagService
    {
        /// <summary>
        /// Hyperlinks the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        string HyperlinkTheTags(string tags, string username);

        /// <summary>
        /// Hyperlinks the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        string HyperlinkTheTags(List<Tag> tags, string username);

        /// <summary>
        /// Hyperlinks the admin tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        string HyperlinkTheAdminTags(string tags, string username, int mediaId);

        /// <summary>
        /// Hyperlinks the admin tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        string HyperlinkTheAdminTags(List<Tag> tags, string username, int mediaId);
    }
}