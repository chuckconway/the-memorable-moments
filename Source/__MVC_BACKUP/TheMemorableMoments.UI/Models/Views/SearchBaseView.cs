using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views
{
    public abstract class SearchBaseView<T> : BaseModel
    {
        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        /// <value>The pagination.</value>
        public IPaginationService<T> Pagination { get; set; }

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        /// <value>The total results.</value>
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText { get; set; }

        /// <summary>
        /// Hyperlinks the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public string HyperlinkTags(string tags, string username)
        {
            ITagService tagService = DependencyInjection.Resolve<ITagService>();
            return tagService.HyperlinkTheTags(tags, username);
        }

        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public string GetImageLink(string set, Media media)
        {
            return PhotoHtmlHelper.GetImageLink(set, media);
        }

    }
}