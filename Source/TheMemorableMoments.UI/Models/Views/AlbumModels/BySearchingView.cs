using System;
using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class BySearchingView : AddAlbumView
    {
        /// <summary>
        /// Gets or sets the partial name of the view.
        /// </summary>
        /// <value>The partial name of the view.</value>
        public string PartialViewName { get; set; }

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

        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        /// <value>The pagination.</value>
        public IPaginationService<Media> Pagination { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the set.
        /// </summary>
        /// <value>The set.</value>
        public string Set { get; set; }

        /// <summary>
        /// Gets or sets the search field.
        /// </summary>
        /// <value>The search field.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public Album Album { get; set; }


        /// <summary>
        /// Gets the album sub navigation.
        /// </summary>
        /// <returns></returns>
        public string GetAlbumSubNavigation()
        {
            StringBuilder builder = new StringBuilder();
            IDictionary<string, string> links = new Dictionary<string, string>
                                                    {
                                                        {"all",  @"<li><a {2} href=""/{0}/albums/addphotos/{1}"">All</a></li>"},
                                                        {"bytags",  @"<li><a {2} href=""/{0}/albums/tags/{1}"">Tags</a></li>"},
                                                        {"bysearching", @"<li><a {2} href=""/{0}/albums/searchphotos/{1}"">Search</a></li>"}
                                                    };

            builder.AppendLine(@"<ul style=""text-align: right; margin-bottom: 40px;"" class=""hyperlinks"" id=""subviewlinks"">");
            foreach (KeyValuePair<string, string> keyValuePair in links)
            {
                string youarehere = (string.Equals(PartialViewName, keyValuePair.Key,
                                                   StringComparison.InvariantCultureIgnoreCase)
                                         ? "class=\"youarehere\""
                                         : string.Empty);
                builder.AppendLine(string.Format(keyValuePair.Value, Authorization.Owner.Username, Album.AlbumId,
                                                 youarehere));
            }
            builder.AppendLine(@"</ul>");
            return builder.ToString();
        }

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
    }
}