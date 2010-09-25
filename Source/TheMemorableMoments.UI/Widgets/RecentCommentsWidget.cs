using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Widgets
{
    public static class RecentCommentsWidget
    {
        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        public static string Render(User user)
        {
            ICommentRepository commentRepository = DependencyInjection.Resolve<ICommentRepository>();
            IMediaRepository mediaRepository = DependencyInjection.Resolve<IMediaRepository>();
            List<Comment> comments = commentRepository.RetrieveRecent5CommentsByUserId(user.Id);
            StringBuilder builder = new StringBuilder();
            string renderedWidget = string.Empty;

            if (comments.Count > 0)
            {
                builder.AppendLine("<div class=\"marginbottom40\">");
                builder.AppendLine("<h3>recent comments</h3>");
                builder.AppendLine("<ul class=\"comments\">");
                const string htmlLink = "<li>{0} on <a href=\"/{1}/comments/leave/{2}\" >{3}</a></li>";
                const string commentorFormat = "<a href=\"{0}\" title=\"Visit {1}\" >{1}</a>";

                foreach (Comment comment in comments)
                {
                    Media media = mediaRepository.RetrieveByPrimaryKeyAndUserId(comment.MediaId, user.Id);

                    string commentor = (string.IsNullOrEmpty(comment.SiteUrl) ? comment.Name : string.Format(commentorFormat, comment.SiteUrl, comment.Name));
                    builder.AppendLine(string.Format(htmlLink, commentor, user.Username, comment.MediaId, (string.IsNullOrEmpty(media.Title) ? "Untitled" : media.Title)));
                }

                builder.AppendLine("</ul>");
                builder.AppendLine("</div>");
                renderedWidget = builder.ToString();
            }

            return renderedWidget;
        }
    }
}
