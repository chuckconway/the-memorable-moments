using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Recent;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.UI.Models.Views
{
    public class UserSearchView : SearchBaseView<Media>, ISidebarView
    {
        /// <summary>
        /// Gets or sets the sidebar tags.
        /// </summary>
        /// <value>The sidebar tags.</value>
        public List<Tag> SidebarTags { get; set; }

        /// <summary>
        /// Gets or sets the sidebar albums.
        /// </summary>
        /// <value>The sidebar albums.</value>
        public List<Album> SidebarAlbums { get; set; }

        /// <summary>
        /// Gets or sets the sidebar friends.
        /// </summary>
        /// <value>The sidebar friends.</value>
        public List<Friend> SidebarFriends { get; set; }

        /// <summary>
        /// Gets or sets the sidebar recent activity.
        /// </summary>
        /// <value>The sidebar recent activity.</value>
        public List<RecentActivity> SidebarRecentActivity { get; set; }

        /// <summary>
        /// Gets or sets the sidebar year with count.
        /// </summary>
        /// <value>The sidebar year with count.</value>
        public List<YearWithCount> SidebarYearWithCount { get; set; }

        /// <summary>
        /// Gets or sets the sidebar recent uploads.
        /// </summary>
        /// <value>The sidebar recent uploads.</value>
        public List<RecentUploads> SidebarRecentUploads { get; set; }

        /// <summary>
        /// Gets or sets the set.
        /// </summary>
        /// <value>The set.</value>
        public string Set { get; set; }

        /// <summary>
        /// Renders the detail column.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        public string RenderDetailColumn(Media m)
        {
            return string.Format("<span style=\"font-size:12px;color:#999;\">{0}</span>", m.Title) +
                   string.Format("<p>{0}</p>", m.Description) +
                   ("<p style=\"margin-bottom:0px;\" >tags: " + HyperlinkTags(m.Tags, m.Owner.Username));
        }
    }
}