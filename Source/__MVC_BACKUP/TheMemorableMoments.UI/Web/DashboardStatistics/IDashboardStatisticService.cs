using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.DashboardStatistics
{
    public interface IDashboardStatisticService
    {
        /// <summary>
        /// Renders the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        string RenderActivity(List<RecentActivityByUser> activity);
    }
}
