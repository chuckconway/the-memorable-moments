using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Models.Views.Dashboard
{
    public class DashboardView : BaseModel
    {
        /// <summary>
        /// Gets the recent friend activity.
        /// </summary>
        /// <value>The recent friend activity.</value>
        public string RecentFriendActivity { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public List<Statistic> Comments { get; set; }

        /// <summary>
        /// Gets or sets the site stats.
        /// </summary>
        /// <value>The site stats.</value>
        public List<Statistic> SiteStats { get; set; }

        /// <summary>
        /// Gets or sets the top ten viewed.
        /// </summary>
        /// <value>The top ten viewed.</value>
        public List<MediaWithViewCount> TopTenViewed { get; set; }

    }
}


