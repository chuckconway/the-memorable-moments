using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views
{
    public class PhotoHtmlHelper
    {

        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public static string GetImageLink(string set, Media media)
        {
            MediaFile thumbnail = media.GetImageByPhotoType(PhotoType.Thumbnail);
            MediaFile websize = media.GetImageByPhotoType(PhotoType.Websize);
            IUserUrlService userUrlService = GetUserUrlService();


            const string linkFormat = "<a id=\"{4}\" class=\"showimage lightbox\" name=\"{3}\" rel=\"{2}\" href=\"{5}\" title=\"{1}\"><img src=\"{0}\" alt=\"{1}\" /></a>";
            string link = string.Format(linkFormat,
                                        userUrlService.CreateImageUrlUsingCloudUrl(media.Owner.Username, thumbnail.FilePath),
                                        media.Title,
                                        userUrlService.CreateImageUrlUsingCloudUrl(media.Owner.Username, websize.FilePath),
                                        media.MediaId,
                                        media.Owner.Username,
                                        userUrlService.UserUrl(media.Owner.Username, "photos/show/" + set + "/#/photo/" + media.MediaId));

            return link;
        }


        /// <summary>
        /// Gets the user URL service.
        /// </summary>
        /// <returns></returns>
        private static IUserUrlService GetUserUrlService()
        {
            return UserUrlService.GetInstance();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> PrivacyList()
        {
            return new List<SelectListItem>
                       {
                                                 new SelectListItem {Value = MediaStatus.Public.ToString(), Text = "Public", Selected = true}, 
                                                 new SelectListItem {Value = MediaStatus.Private.ToString(), Text = "Private"},
                                                 new SelectListItem {Value = MediaStatus.InNetwork.ToString(), Text = "In Network"},
                                             };
        }


        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="set">The set.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetImageDetailLinkForFirst(Media media, string cssClass, string set, PhotoType type)
        {
            IUserUrlService userUrlService = GetUserUrlService();
            Func<string, string> title =
                s => (string.IsNullOrEmpty(media.Title)
                     ? string.Empty
                     : string.Format("title=\"{0} - {1} {2}\"", media.Title, media.Owner.FirstName, media.Owner.LastName));

            MediaFile thumbnail = media.GetImageByPhotoType(type);
            const string linkFormat = "<a class=\"{5} lightbox\" name=\"{6}\"  href=\"{0}\" {1} ><img src=\"{2}\" alt=\"{3}\" {4} /></a>";
            string link = string.Format(linkFormat,
                userUrlService.UserUrl(media.Owner.Username, "photos/show/" + set + "/#/photo/" + media.MediaId),
                title(media.Title),
                userUrlService.CreateImageUrl(media.Owner.Username, thumbnail.FilePath),
                media.Title,
                title(media.Title),
                cssClass,
                media.MediaId);

            return link;
        }


        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public static string GetThumbnailImage(Media media)
        {
            string link = string.Empty;
            
            if (media != null)
            {
                IUserUrlService userUrlService = GetUserUrlService();

                MediaFile thumbnail = media.GetImageByPhotoType(PhotoType.Thumbnail);
                const string linkFormat = "<img src=\"{0}\" alt=\"{1}\" /></a>";
                link = string.Format(linkFormat, userUrlService.CreateImageUrl(media.Owner.Username, thumbnail.FilePath), media.Description);
            }

            return link;
        }
        
        /// <summary>
        /// Gets the image link.
        /// </summary>
        /// <param name="friend">The friend.</param>
        /// <returns></returns>
        public static string GetFriendImageThumbnailForUserHomepage(Friend friend)
        {
            string val = string.Empty;

            if (friend.Media != null)
            {
                IUserUrlService userUrlService = GetUserUrlService();

                MediaFile thumbnail = friend.Media.GetImageByPhotoType(PhotoType.Thumbnail);
                const string linkFormat = "<li><span><a class=\"showimage\" href=\"{0}\" title=\"{1}\"><img src=\"{2}\" alt=\"{3}\" /></a></span><span class=\"albumtitle\">{4}<span></li>";
                val = string.Format(linkFormat,
                    userUrlService.UserRoot(friend.Username), 
                    friend.DisplayName, 
                    userUrlService.CreateImageUrl(friend.Media.Owner.Username,thumbnail.FilePath), 
                    friend.Media.Description, 
                    friend.DisplayName);
            }
            return val;
        }


        /// <summary>
        /// Gets the photo detail links.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <returns></returns>
        public static string GetPhotoDetailLinks(Media media, bool isAuthenticated)
        {
            IUserUrlService userUrlService = GetUserUrlService();
            ITagService tagService = DependencyInjection.Resolve<ITagService>();

            const string perminateLinkFormat = @"<li><a href=""{0}"" title=""{1}"" >permalink</a></li>";
            string perminateLink = string.Format(perminateLinkFormat, userUrlService.UserUrl(media.Owner.Username, "photos/show/" + media.MediaId), media.Title);
            
            string html = @"<ul>
                        <li>
                        <span>";
            html += (isAuthenticated ? @"<a id=""editlink""  href=""{0}/photos/edit/{1}"">edit</a>" : string.Empty);
            html += @"</span>            
                    </li>";
            html += "{2}";
            html += @"          
                    </li>
                     {3}  
                     {5}                  
                    <li><span><a href=""{0}/comments/leave/{1}"">comments ({4})</a></span> 
                </ul>";

            string tags = string.Empty;
            if (!string.IsNullOrEmpty(media.Tags))
            {
                const string tagFormat = @"<li><span>tags:</span> {0}</li>";
                string renderedTags = tagService.HyperlinkTheTags(media.Tags, media.Owner.Username);
                tags = string.Format(tagFormat, renderedTags);
            }

            string date = GetDate(media);

            string content = string.Format(html, userUrlService.UserRoot(media.Owner.Username), HttpUtility.HtmlEncode(media.MediaId.ToString()), date, tags, media.CommentCount, perminateLink);
            return content;
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        internal static string GetDate(Media media)
        {
            string date = string.Empty;
            if(media.Year.HasValue || media.Month.HasValue || media.Day.HasValue)
            {
                const string format = "<li><span class=\"tagcolor\" ></span><span>{0}</span></li>";

                if (media.Year.HasValue && 
                    media.Month.HasValue && 
                    media.Day.HasValue)
                {
                    DateTime mediaDate = new DateTime(media.Year.GetValueOrDefault(), media.Month.GetValueOrDefault(), media.Day.GetValueOrDefault());
                    date = string.Format(format, mediaDate.ToLongDateString());
                }

                if (media.Year.HasValue && 
                    media.Month.HasValue && 
                    !media.Day.HasValue)
                {
                    DateTime mediaDate = new DateTime(media.Year.GetValueOrDefault(), media.Month.GetValueOrDefault(), 1);
                    date = string.Format(format, mediaDate.ToString("MMMM, yyyy"));
                }

                if (media.Year.HasValue && 
                    !media.Month.HasValue && 
                    !media.Day.HasValue){ date = string.Format(format, "year: " + media.Year); }
            }

            return date;
        }
    }
}
