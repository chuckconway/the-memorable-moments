using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Widgets
{
    public static class TagsWidget
    {
        /// <summary>
        /// Renders the specified tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static string Render(IList<Tag> tags, string username)
        {
            IUserUrlService urlService = UserUrlService.GetInstance();
            StringBuilder builder = new StringBuilder();

            if (tags == null) { throw new System.ArgumentNullException("tags", "Parameter 'tags' is null, value expected"); }

            if (tags.Count > 0)
            {
                builder.AppendLine(string.Format("<div class=\"marginbottom40\" ><h3 class=\"tagallhack\" ><a href=\"{0}\">tags</a></h3> ", urlService.CreateUrl(username, "tags")));
                builder.AppendLine("<p style='text-align:right;margin-top:3px;' ><label id='filtertags' >filter:</label><input type='text' id='tagfilter' /></p> ");

                const string tagFormat = "<span class=\"tagWrapper\" ><a href=\"/{0}/tagged/{1}\" rel=\"{2}\" title=\"view photos tagged '{1}'\" class=\"tag\">{1}</a>{3}</span>";
                for (int index = 0; index < tags.Count; index++)
                {
                    builder.AppendLine(string.Format(tagFormat, username, tags[index].TagText, tags[index].Count,(index < tags.Count - 1 ? "<span class=\"comma\" >,</span>" : string.Empty)));
                }

                builder.AppendLine("</div>");
            }

            return builder.ToString();
        }
    }
}
