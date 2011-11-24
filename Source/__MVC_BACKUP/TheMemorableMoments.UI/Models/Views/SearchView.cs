using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views
{
    public class SearchView : SearchBaseView<Media>
    {
        public string Set { get; set; }

        /// <summary>
        /// Renders the detail column.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        public string RenderDetailColumn(Media m)
        {
            return string.Format("<span style=\"font-size:12px;color:#999;\">{0}</span>",  m.Title) +
                   string.Format("<p>{0}</p>",  m.Description) +
                   ("<p style=\"margin-bottom:0px;\" >tags: " + HyperlinkTags(m.Tags, m.Owner.Username) + " <br />view more of <a href=\"/" + m.Owner.Username + "\">" + m.Owner.FirstName + " " +
                    m.Owner.LastName + "</a> photos</p>");
        }


    }
}
