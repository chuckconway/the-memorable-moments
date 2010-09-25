using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Web.Recent;

namespace TheMemorableMoments.UI.Web.DashboardStatistics
{
    public class DashboardStatisticService : IDashboardStatisticService
    {
        /// <summary>
        /// Renders the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        public string RenderActivity(List<RecentActivityByUser> activity)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string actvity in activity.Select(RenderActivity).Where(actvity => !string.IsNullOrEmpty(actvity)))
            {
                builder.AppendLine("<li>" + actvity + "</li>");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Renders the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        public string RenderActivity(RecentActivity activity)
        {
            string val = string.Empty;
            IEnumerable<IRecentActivity> activities = GetActivities();

            foreach (IRecentActivity recentActivity in activities.Where(recentActivity => activity.ActivityType == recentActivity.ActivityType))
            {
                val = recentActivity.Render(activity);
                break;
            }

            return val;
        }

        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IRecentActivity> GetActivities()
        {
            return new List<IRecentActivity>
                       {
                           new NewAlbumActivity(),
                           new PhotoRecentActivity(),
                           new FriendsAddedActivity(),
                           new PhotosAddedToAlbumActivity(),
                           new TagsAddedActivity()
                       };
        }
    }
}