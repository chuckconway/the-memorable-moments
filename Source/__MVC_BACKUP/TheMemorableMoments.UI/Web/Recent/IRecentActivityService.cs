using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Recent
{
    public interface IRecentActivityService
    {
        /// <summary>
        /// Renders the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        string RenderActivity(RecentActivity activity);
    }
}