using System.Collections.Generic;
using System.Text;
using System.Linq;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Web.Recent;

namespace TheMemorableMoments.UI.Widgets
{
    public static class RecentActivityWidget 
    {
        /// <summary>
        /// Renders the specified recent activities.
        /// </summary>
        /// <param name="recentActivities">The recent activities.</param>
        /// <returns></returns>
        public static string Render(IList<RecentActivity> recentActivities)
        {
            StringBuilder builder = new StringBuilder();
            int sum = recentActivities.Sum(o => o.Count);

            if (recentActivities == null) { throw new System.ArgumentNullException("recentActivities", "Parameter 'recentActivities' is null, value expected"); }

            if (recentActivities.Count > 0 && sum > 0)
            {    
                IRecentActivityService service = DependencyInjection.Resolve<IRecentActivityService>();
                builder.AppendLine("<h3>recent activity</h3>");
                builder.AppendLine("<ul  class=\"comments lightgrey\">");

                foreach (RecentActivity activity in recentActivities.Where(activity => activity.Count > 0))
                {
                    builder.AppendLine(string.Format("<li>{0}</li>", service.RenderActivity(activity)));
                }

                builder.AppendLine("</ul>");
            }

            return builder.ToString();
        }
    }
}
