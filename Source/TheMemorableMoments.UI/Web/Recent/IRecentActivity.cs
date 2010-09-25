using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Recent
{
    public interface IRecentActivity
    {
        /// <summary>
        /// Gets the type of the activity.
        /// </summary>
        /// <value>The type of the activity.</value>
        ActivityType ActivityType { get;}

        /// <summary>
        /// Renders the specified recent activity.
        /// </summary>
        /// <param name="recentActivity">The recent activity.</param>
        /// <returns></returns>
        string Render(RecentActivity recentActivity);
    }
}