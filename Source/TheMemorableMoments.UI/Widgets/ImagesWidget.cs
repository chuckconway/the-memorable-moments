using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Widgets
{
    public static class ImagesWidget
    {
        /// <summary>
        /// Renders the specified media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="user">The user.</param>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        public static string Render(IList<Media> media, User user, string set)
        {
            IUserUrlService urlService = UserUrlService.GetInstance(user);
            StringBuilder builder = new StringBuilder();

            const string thumbFormat = @"
                                        <div class=""thumbnails"" {3}>
                                            <span >
                                                <a href=""{9}"" rel=""{0}"" name=""{4}"" class=""showimage lightbox"" title=""{2}"" ><img src=""{1}""  alt=""{2}"" /></a>
                                            </span>
                                        
                                            <ul class=""largethumbnailmetadata"" >
                                                <li class=""title"" >{5}</li>
                                                <li>{6}</li>
                                                <li class=""comment hyperlinks"" ><a href=""{8}/comments/leave/{4}"">comments ({7})</a></li>
                                            </ul>
                                        </div>";

            if (media == null){ throw new System.ArgumentNullException("media","Parameter 'media' is null, value expected");}

            if (media.Count > 0)
            {
                string imagePath = PhotoHtmlHelper.GetImageDetailLinkForFirst(media[0], "homepagefullsize", set, PhotoType.Websize);
                builder.AppendLine(string.Format("<div class=\"firstimage\" >{0} <div > <h3 class=\"firstphototitle\" >{1}</h3> <p>{2}</p></div> <br class=\"clearboth\" /> </div>", imagePath, media[0].Title, media[0].Description));
                
                if(media.Count > 15)
                {
                    
                    builder.AppendLine("<div class=\"largethumbs\" >");

                    for (int index = 1; index < media.Count && index < 16; index++)
                    {
                        string showUrl = urlService.UserUrl("photos/show/" + set + "/#/photo/" + media[index].MediaId);
                        MediaFile webSize = media[index].GetImageByPhotoType(PhotoType.Websize);
                        string websizeUrl = urlService.CreateImageUrl(webSize.FilePath);
                        builder.AppendLine(string.Format(thumbFormat, 
                            websizeUrl, 
                            websizeUrl, 
                            media[index].Title, 
                            (index % 3 == 0 ? "style=\"margin-right:0px;\"" : string.Empty), 
                            media[index].MediaId,
                            TruncateText(media[index].Title, 20),
                            TruncateText(media[index].Description, 50),
                            media[index].CommentCount,
                            urlService.UserRoot(),
                            showUrl));
                    }

                    builder.AppendLine("<div class=\"clearboth\" ></div>");
                    builder.AppendLine("</div>");
                }

            }

            return builder.ToString();
        }

        /// <summary>
        /// Truncates the text.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string TruncateText(string val, int length)
        {
            if (val.Length > length)
            {
                val = val.Trim().Substring(0, length) + "...";
            }

            return val;
        }
    }
}
