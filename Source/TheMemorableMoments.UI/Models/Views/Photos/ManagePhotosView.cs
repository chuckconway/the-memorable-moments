using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views.Photos
{
    public class ManagePhotosView : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
// ReSharper disable InconsistentNaming
        public Func<string, string, int, string> RenderAdminTags;

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        /// <value>The total results.</value>
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the partial view.
        /// </summary>
        /// <value>The partial view.</value>
        public string PartialView { get; set; }
// ReSharper restore InconsistentNaming
        
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        /// <value>The pagination.</value>
        public IPaginationService<Media> Pagination { get; set; }

        /// <summary>
        /// Gets or sets the name of the tab.
        /// </summary>
        /// <value>The name of the tab.</value>
        public string TabName { get; set; }
        
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the media status count.
        /// </summary>
        /// <value>The media status count.</value>
        public List<StatusCount<MediaStatus>> MediaStatusCount { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the set.
        /// </summary>
        /// <value>The set.</value>
        public string Set { get; set; }

        /// <summary>
        /// Gets the image link.`
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public string GetImageLink(string set, Media media)
        {
            return PhotoHtmlHelper.GetImageLink(set, media);
        }

        /// <summary>
        /// Renders the manage photo tabs.
        /// </summary>
        /// <returns></returns>
        public string RenderManageTabs()
        {
            string html;

            if (!string.Equals(PartialView, "SearchView", StringComparison.InvariantCultureIgnoreCase))
            {

                html = string.Format("<li><a class=\"youarehere\" href=\"/{0}/photos/\">Photos</a></li>" +
                       "<li><a href=\"/{0}/photos/search\">Search</a></li>", Authorization.Owner.Username);
            }
            else
            {
                html = string.Format("<li><a  href=\"/{0}/photos/\">Photos</a></li>" +
                                    "<li><a class=\"youarehere\" href=\"/{0}/photos/search\">Search</a></li>", Authorization.Owner.Username);
            }
            
            return html;
        }

        /// <summary>
        /// Renders the manage photos tabs.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="mediaStatusCount">The media status count.</param>
        /// <returns></returns>
        public static string RenderManagePhotosLinks(string username, string tabName, List<StatusCount<MediaStatus>> mediaStatusCount)
        {
            StringBuilder builder = new StringBuilder();
            IEnumerable<Tuple<string, string>> nav = GetTabs();

            const string format = "<a {3} href=\"/{0}/photos/{1}\">{2}</a>";
            foreach (Tuple<string, string> t in nav)
            {
                int tabCount = GetTabCount(t.Item1, mediaStatusCount);

                string tabText = string.Format("{0} ({1})", t.Item2, tabCount);
                builder.AppendLine(string.Format("<li>{0}</li>",
                                                 string.Format(format, username, t.Item1, tabText, (string.Equals(tabName, t.Item1) ? "class=\"current\"" : string.Empty))));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the tab count.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="mediaStatusCount">The media status count.</param>
        /// <returns></returns>
        private static int GetTabCount(string tabName, IEnumerable<StatusCount<MediaStatus>> mediaStatusCount)
        {
            return (from statusCount in mediaStatusCount
                    where string.Equals(tabName, statusCount.Status.ToString(), StringComparison.InvariantCultureIgnoreCase)
                    select statusCount.Count).FirstOrDefault();
        }

        /// <summary>
        /// Gets the current tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <returns></returns>
        public string GetCurrentTab(string tabName)
        {
            IEnumerable<Tuple<string, string>> nav = GetTabs();
            string tab = string.Empty;

            foreach (Tuple<string, string> list in nav.Where(list => string.Equals(tabName, list.Item1)))
            {
                tab = list.Item2;
                break;
            }

            return tab.ToLower();
        }

        /// <summary>
        /// Renders the edit links.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        public string RenderEditLinks(Media m)
        {
            return string.Format("<a class=\"delete\" name=\"{1}\" style='float: right;margin-right:10px;' href=\"/{0}/photos/delete/{1}\" title=\"delete photo\"><img style='background-color:transparent;border:none;' src='/content/images/cancel.png' alt='delete photo' /></a>", Authorization.Owner.Username, m.MediaId) +
                 string.Format("<a class=\"edit\" name=\"{1}\" style='float: right;margin-right:30px;' href=\"/{0}/photos/edit/{1}\" title=\"edit photo\"><img style='background-color:transparent;border:none;' src='/content/images/pencil.png' alt='edit' /></a>", Authorization.Owner.Username, m.MediaId);

        }

        /// <summary>
        /// Renders the details.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        public string RenderDetails(Media m)
        {
            return string.Format("<span class=\"title\" >{0}</span>", m.Title) +
             string.Format("<p>{0}</p>", m.Description) + (string.IsNullOrEmpty(m.Tags) ? string.Empty : "<p style=\"margin-bottom:0px;\" >tags:" + RenderAdminTags(m.Tags, Authorization.Owner.Username, m.MediaId) + " </p>");
        }


        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Tuple<string, string>> GetTabs()
        {
            return new List<Tuple<string, string>>
                       {
                           new Tuple<string, string>("public", "Public"),
                           new Tuple<string, string>("innetwork", "In Network"),
                           new Tuple<string, string>("private", "Private")
                       };
        }

    }
}