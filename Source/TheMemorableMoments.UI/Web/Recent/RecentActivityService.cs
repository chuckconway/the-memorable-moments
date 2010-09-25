using System.Collections.Generic;
using System.Linq;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Recent
{
    public class RecentActivityService : IRecentActivityService
    {
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
        public virtual  IEnumerable<IRecentActivity> GetActivities()
        {
            return new List<IRecentActivity>
                       {
                           new CommentActivity(),
                           new FriendsAddedActivity(),
                           new NewAlbumActivity(),
                           new PhotoRecentActivity(),
                           new PhotosAddedToAlbumActivity(),
                           new TagsAddedActivity()
                       };
        }
    }
}
