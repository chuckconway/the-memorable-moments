using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views.Comments
{
    public class ManageCommentsView : BaseModel
    {

        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        /// <value>The pagination.</value>
        public IPaginationService<MediaComments> Pagination { get; set; }

        private readonly List<Tuple<string, string, CommentStatus?>> _linkFormats = new List<Tuple<string, string, CommentStatus?>>
                                                             {
                                                                 new Tuple<string, string, CommentStatus?>("all", "All" , null),
                                                                 new Tuple<string, string, CommentStatus?>("pending","Pending", CommentStatus.Pending),
                                                                 new Tuple<string, string, CommentStatus?>("approved","Approved", CommentStatus.Approved),
                                                                 new Tuple<string, string, CommentStatus?>("spam","Spam", CommentStatus.Spam)
                                                             };

        /// <summary>
        /// Renders the view navigation.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public string RenderViewNavigation(string view, User user)
        {
            const string linkFormat = @"<li><a {1} href=""/{0}/comments/{3}"">{2}</a> </li>";
            StringBuilder builder = new StringBuilder(@"<ul id=""subviewlinks"" class=""hyperlinks"">");

            foreach (Tuple<string, string, CommentStatus?> format in _linkFormats)
            {
                string current = (string.Equals(format.Item1, view) ? "class=\"current\"" : string.Empty);
                string count = (format.Item3 != null
                                    ? "(" + CommentCountByType(CommentStatuses, format.Item3.GetValueOrDefault()) +")"
                                    : string.Empty);
                builder.AppendLine(string.Format(linkFormat, user.Username, current, string.Format("{0} {1}", format.Item2, count), format.Item1));
            }

            builder.AppendLine("</ul>");
            return builder.ToString();
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The view.</value>
        public string View { get; set; }

        /// <summary>
        /// Gets or sets the media status count.
        /// </summary>
        /// <value>The media status count.</value>
        public List<StatusCount<MediaStatus>> MediaStatusCount { get; set; }


        /// <summary>
        /// Gets or sets the comment statuses.
        /// </summary>austin1234
        /// <value>The comment statuses.</value>
        public List<StatusCount<CommentStatus>> CommentStatuses { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage {get; set; }

        /// <summary>
        /// Comments the type of frefthe count by.
        /// </summary>
        /// <param name="commentStatuses">The comment statuses.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        public int CommentCountByType(List<StatusCount<CommentStatus>> commentStatuses, CommentStatus commentType)
        {
            return (from statusCount in commentStatuses where statusCount.Status == commentType select statusCount.Count).FirstOrDefault();
        }

        /// <summary>
        /// Renders the comments.
        /// </summary>
        /// <param name="comments">The comments.</param>
        /// <returns></returns>
        public  string RenderComments(List<Comment> comments)
        {
            string commentsHtml;

            if (comments != null && comments.Count > 0)
            {
                commentsHtml = RenderCommentsHtml(comments);
            }
            else
            {
                commentsHtml = @"<p class=""nocommentsmessage""><strong>No comments</strong> posted</p>";
            }

            return commentsHtml;
        }

        /// <summary>
        /// Renders the comments HTML.
        /// </summary>
        /// <param name="comments">The comments.</param>
        /// <returns></returns>
        private static string RenderCommentsHtml(IList<Comment> comments)
        {
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < comments.Count; index++)
            {
                const string commentHtml = @"<div class=""commentsection{0}"">
                                                <p class=""commentdate"">{2}</p>
                                                <p class=""commentauthor"">{1}</p>                                     
                                                <div class=""postedcomment"">{3}</div>
                                            </div>";

                builder.AppendFormat(commentHtml, (index % 2 == 1 ? "" : " altcomment"),
                                     comments[index].Name + " said:",
                                     comments[index].CommentDate.ToLongDateString(),
                                     comments[index].Text);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the image HTML.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public string GetImageHtml(Media media)
        {
            const string htmlFormat = @"<img alt=""{0}"" id=""image"" src=""{1}"">";

            MediaFile mediaFile = media.GetImageByPhotoType(PhotoType.Websize);
            return string.Format(htmlFormat, media.Title, UrlService.CreateImageUrl(mediaFile.FilePath));
        }


        /// <summary>
        /// Renders the author details.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public string RenderAuthorDetails(Comment comment)
        {
            return string.Format(" {0}{1}{2}{3}", RenderCommentStatus(comment),
                                 (string.IsNullOrEmpty(comment.Name) ? string.Empty : (string.IsNullOrEmpty(comment.SiteUrl) ? "<br />" +comment.Name : string.Format("<br /><a href=\"{0}\" title=\"Visit {1}\" >{1}</a>", comment.SiteUrl, comment.Name))),
                                 (string.IsNullOrEmpty(comment.Email) ? string.Empty : string.Format("<br /><a href=\"mailto:{0}\" title=\"Email {1}\" >{2}</a>", comment.Email, comment.Name, comment.Email)),
                                 (string.IsNullOrEmpty(comment.Ip) ? string.Empty : "<br />" + comment.Ip));
        }


        /// <summary>
        /// Renders the comment status.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public string RenderCommentStatus(Comment comment)
        {
            const string statusFormat = "<span class=\"{0}\">{1}</span>";

            string status = string.Format(statusFormat, "green", comment.CommentStatus);
            status = (comment.CommentStatus == CommentStatus.Pending ? string.Format(statusFormat, "orange", comment.CommentStatus) : status);
            status = (comment.CommentStatus == CommentStatus.Spam ? string.Format(statusFormat, "red", comment.CommentStatus) : status);

            return status;
        }
    }
}


