using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.Recent;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.UI.Models.Views
{
    public class SidebarView : BaseModel, ISidebarView
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


    }
}