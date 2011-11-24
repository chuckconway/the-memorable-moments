using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Recent
{
    public class NewAlbumActivity : IRecentActivity
    {
        /// <summary>
        /// Gets the type of the activity.
        /// </summary>
        /// <value>The type of the activity.</value>
        public ActivityType ActivityType
        {
            get { return ActivityType.NewAlbums; }
        }

        /// <summary>
        /// Renders the specified recent activity.
        /// </summary>
        /// <param name="recentActivity">The recent activity.</param>
        /// <returns></returns>
        public string Render(RecentActivity recentActivity)
        {
            return string.Format("{0} album(s) created.", recentActivity.Count);
        }
    }
}
