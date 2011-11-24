using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views.AlbumModels
{
    public class AlbumManageView : BaseModel
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
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public List<Media> Media { get; set; }

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
        /// Gets the media details.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public string GetMediaDetails(Media media)
        {
            return string.Format("{0}{1}<p style=\"margin-bottom:0px;\" >tags: {2}", string.Format("<strong style=\"font-size:11px;color:#bbb;\">{0}</strong>", media.Title), string.Format("<p>{0}</p>", media.Description), HyperlinkTags(media.Tags, media.Owner.Username));
        }

        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public string JustTheImageWebsize(Media media)
        {
            MediaFile thumbnail = media.GetImageByPhotoType(PhotoType.Thumbnail);
            MediaFile websize = media.GetImageByPhotoType(PhotoType.Websize);


            const string linkFormat = "<a class=\"popupimage\" href=\"{0}\" name=\"{4}\" title=\"{1}\"><img src=\"{2}\" alt=\"{3}\" /></a>";
            string link = string.Format(linkFormat,
                                        UrlService.CreateImageUrlUsingCloudUrl(media.Owner.Username, websize.FilePath),
                                        media.Title,
                                       UrlService.CreateImageUrlUsingCloudUrl(media.Owner.Username, thumbnail.FilePath),
                                        media.Description, media.MediaId);

            return link;
        }
    }
}