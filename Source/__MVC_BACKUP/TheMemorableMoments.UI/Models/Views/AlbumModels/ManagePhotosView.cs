using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class ManagePhotosView : ManageViewBase
    {
        /// <summary>
        /// Gets or sets the album crumbs.
        /// </summary>
        /// <value>The album crumbs.</value>
        public string AlbumCrumbs { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the set.
        /// </summary>
        /// <value>The set.</value>
        public string Set { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public List<MediaWithAlbumPosition> Media { get; set; }

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

        /// <summary>
        /// Renders the action links.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public string RenderActionLinks(Media media)
        {
            bool isCoverImage = (media.MediaId == Album.CoverMediaId.GetValueOrDefault());
            string coverImageHtml = (isCoverImage
                                         ? "<span id=\"coverimage\" >cover image</span>"
                                         : string.Format(
                                             "<a class=\"setcoverimage\" href=\"/{0}/albums/SetCoverImage/?mediaid={1}&albumid={2}\" >set as cover image</a>",
                                             Authorization.Owner.Username, media.MediaId, Album.AlbumId));

            string html = string.Format("{3} | <a class=\"redhover\" href=\"/{0}/albums/RemoveMediaFromAlbum/?mediaid={1}&albumid={2}\" >remove</a>",
                                          Authorization.Owner.Username, media.MediaId, Album.AlbumId, coverImageHtml);
            return html;
        }

    }
}