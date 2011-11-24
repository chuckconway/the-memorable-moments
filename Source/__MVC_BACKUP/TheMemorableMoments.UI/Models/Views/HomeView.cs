using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views
{
    public class HomeView : BaseModel
    {
        /// <summary>
        /// Gets or sets the meida.
        /// </summary>
        /// <value>The meida.</value>
        public List<Media> Media { get; set; }

        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public  string GetImageLink(Media media)
        {
            Func<string, string> title =
                s => (string.IsNullOrEmpty(media.Title)
                     ? string.Empty
                     : string.Format("title=\"{0} - {1} {2}\"", media.Title, media.Owner.FirstName, media.Owner.LastName));

            MediaFile thumbnail = media.GetImageByPhotoType(PhotoType.Thumbnail);
            MediaFile websize = media.GetImageByPhotoType(PhotoType.Websize);
            const string linkFormat = "<a class=\"{5}\" id=\"{7}\" name=\"{6}\" rel=\"{0}\" href=\"{8}\" {1} ><img src=\"{2}\" alt=\"{3}\" {4} /></a>";
            string link = string.Format(linkFormat,
                UrlService.CreateImageUrl(media.Owner.Username, websize.FilePath),
                title(media.Title),
                UrlService.CreateImageUrl(media.Owner.Username, thumbnail.FilePath),
                media.Title,
                title(media.Title),
                "lightbox showimage",
                media.MediaId, 
                media.Owner.Username,
                UrlService.UserUrl(media.Owner.Username, "photos/show/#/photo/" + media.MediaId));

            return link;
        }
    }
}
