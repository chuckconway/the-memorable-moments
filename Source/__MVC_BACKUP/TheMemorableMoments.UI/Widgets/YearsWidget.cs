using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.Domain.Model.Recent;

namespace TheMemorableMoments.UI.Widgets
{
    public class YearsWidget
    {
        /// <summary>
        /// Renders the specified years.
        /// </summary>
        /// <param name="years">The years.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static string Render(IList<YearWithCount> years, string username)
        {
            StringBuilder builder = new StringBuilder();

            if (years == null) { throw new System.ArgumentNullException("years", "Parameter 'tags' is null, value expected"); }

            if (years.Count > 0)
            {
                builder.AppendLine("<div class=\"marginbottom40\" ><h3 class=\"tagallhack\" >years</h3> ");
                builder.AppendLine("<p style='text-align:right;margin-top:3px;' ><label id='filtertags' >filter:</label><input type='text' id='yearfilter' /></p> ");

                const string tagFormat = "<span class=\"yearWrapper\" ><a href=\"/{0}/year/{1}\" rel=\"{2}\" title=\"view photos in '{1}'\" class=\"year\">{1}</a>{3}</span>";
                for (int index = 0; index < years.Count; index++)
                {
                    builder.AppendLine(string.Format(tagFormat, username, years[index].MediaYear, years[index].Count, (index < years.Count - 1 ? "<span class=\"comma\" >,</span>" : string.Empty)));
                }

                builder.AppendLine("</div>");
            }

            return builder.ToString();
        }
    }
}