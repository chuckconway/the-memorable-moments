using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views
{
    public abstract class BaseModel : IBaseModel
    {

        /// <summary>
        /// Gets or sets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public Authorization Authorization { get; set; }

        /// <summary>
        /// Gets or sets the bread crumbs.
        /// </summary>
        /// <value>The bread crumbs.</value>
        public List<BreadCrumb> BreadCrumbs { get; set; }

        /// <summary>
        /// Gets or sets the URL service.
        /// </summary>
        /// <value>The URL service.</value>
        public IUserUrlService UrlService { get; set; }

        /// <summary>
        /// Renders the bread crumbs.
        /// </summary>
        /// <returns></returns>
        public string RenderBreadCrumbs()
        {
            return GetBreadCrumbs(BreadCrumbs);
        }
        

        /// <summary>
        /// Gets the breadcrumbs.
        /// </summary>
        /// <param name="keyValPair">The key val pair.</param>
        /// <returns></returns>
        private static string GetBreadCrumbs(IList<BreadCrumb> keyValPair)
        {
            const string userLink = "<label id=\"breadcrumbs\">";
            const string crumbLink = "{0}<span><a class=\"hyperlinks\" style=\"border-top:none;\" href=\"{1}\" >{2}</a></span> ";
            StringBuilder builder = new StringBuilder();

            if (keyValPair.Count > 0)
            {
                builder.AppendLine(string.Format(userLink));

                for (int index = 0; index < keyValPair.Count; index++)
                {
                    if (index == 0)
                    {
                        string link = string.Format(crumbLink, string.Empty, keyValPair[index].Href, keyValPair[index].Text);
                        builder.AppendLine(link);
                    }

                    if (!string.IsNullOrEmpty(keyValPair[index].Text) && index > 0)
                    {
                        string link = (!string.IsNullOrEmpty(keyValPair[index].Href)
                                           ? string.Format(crumbLink, "> ", keyValPair[index].Href,
                                                           keyValPair[index].Text)
                                           : string.Format("> <span>{0}</span>", keyValPair[index].Text));
                        builder.AppendLine(link);
                    }
                }

                builder.AppendLine("</label>");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets the image SRC.
        /// </summary>
        /// <returns></returns>
        public string GetImageSrc(Media media, PhotoType type)
        {
            MediaFile webSize = media.GetImageByPhotoType(type);
            string url = UrlService.CreateImageUrl(media.Owner.Username, webSize.FilePath);
            return url;
        }

    }
}