using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Recent;
using System.Linq;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Widgets
{
    public class RecentUploadsWidget
    {
        private static readonly IUserUrlService _urlService = UserUrlService.GetInstance();

        /// <summary>
        /// Renders the specified albums.
        /// </summary>
        /// <param name="uploads">The uploads.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static string Render(IList<RecentUploads> uploads, string username)
        {
            StringBuilder builder = new StringBuilder();
            uploads = uploads.OrderByDescending(o => o.CreateDate).ToList();

            if (uploads == null) { throw new System.ArgumentNullException("uploads", "Parameter 'uploads' is null, value expected"); }

            if (uploads.Count > 0)
            {
                string albumUrl = _urlService.CreateUrl(username, "recent");
                builder.AppendLine(string.Format("<h3><a href=\"{0}\" >recent uploads</a></h3>", albumUrl));
                string val = BuildUpload(uploads);

                builder.AppendLine("<table class=\"recent\" ><tbody>");
                builder.AppendLine(val);
                builder.AppendLine("</tbody></table>");
                builder.AppendLine(uploads.Count > 4
                                       ? string.Format("<p class=\"clearboth marginbottom40\" ><a href=\"{0}/#/\" title=\"view additional uploads\">more...</a></p>", albumUrl)
                                       : "<br class=\"clearboth\" />");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Builds the album.
        /// </summary>
        /// <param name="uploads">The uploads.</param>
        /// <returns></returns>
        private static string BuildUpload(IList<RecentUploads> uploads)
        {
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < uploads.Count; index++)
            {
                if (index < 4)
                {
                    builder.AppendLine(RenderRecentUploadHtml(uploads[index]));
                }
                else
                {
                    break;
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="upload">The upload.</param>
        /// <returns></returns>
        public static string RenderRecentUploadHtml(RecentUploads upload)
        {
            string val = string.Empty;

            if (upload != null)
            {
                MediaFile thumbnail = upload.GetImageByPhotoType(PhotoType.Thumbnail);
                MediaFile webSize = upload.GetImageByPhotoType(PhotoType.Websize);
                const string linkFormat = @"<tr>
                                                <td><span class=""imagecontainer"" ><a rel=""{0}"" href=""{7}"" class=""showimage lightbox"" title=""{1}""><img src=""{2}"" alt=""{3}"" /></a></span></td>
                                                <td>
                                                    <ul class=""albummetadata"" >
                                                        <li><label class=""albumtitle"" ><span>{4}</span></label></li>
                                                        <li><abbr class=""timeago"" title=""{5}"">{6}</abbr></li>  
                                                    </ul>              
                                                </td>
                                            </tr>";
                val = string.Format(linkFormat,
                     _urlService.CreateImageUrl(upload.Owner.Username, webSize.FilePath),
                     upload.Title,
                    _urlService.UserUrl(upload.Owner.Username, "services/grayscale/" + thumbnail.MediaId),
                    upload.Title,
                    upload.Title,
                    upload.CreateDate.ToString("o"), 
                    upload.CreateDate.ToLongDateString(),
                    _urlService.UserUrl(upload.Owner.Username, "photos/show/#/photo/" + upload.MediaId));
            }
            return val;
        }
    }
}