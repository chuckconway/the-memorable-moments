using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views.Dashboard;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.DashboardStatistics;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class DashboardController : DashboardBaseController
    {
        private readonly IRecentRepository _recentActivity;
        private readonly IDashboardStatisticService _statisticService;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IReportingRepository _reportingRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="recentActivity">The recent activity.</param>
        /// <param name="statisticService">The statistic service.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="reportingRepository">The reporting repository.</param>
        public DashboardController(IRecentRepository recentActivity, 
            IDashboardStatisticService statisticService, 
            IUserRepository userRepository, 
            ICommentRepository commentRepository, 
            IReportingRepository reportingRepository)
        {
            _recentActivity = recentActivity;
            _reportingRepository = reportingRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _statisticService = statisticService;
        }

        /// <summary>
        /// Loads the Index View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<RecentActivityByUser> retrieveLastSevenDaysOfFriendsActivity = _recentActivity.RetrieveLastSevenDaysOfFriendsActivity(Owner.Id);
            string recentFriends = RenderRecentFriends(retrieveLastSevenDaysOfFriendsActivity);
            IDictionary<string, string> crumbs = HomeBreadCrumb();
            IList<CommentStatus> commentStatuses = EnumerationsExtensions.GetValues<CommentStatus>();

            DashboardView dashboardView = new DashboardView
                                              {
                                                  RecentFriendActivity = recentFriends,
                                                  Comments = GetCommentCounts(commentStatuses),
                                                  SiteStats = _reportingRepository.SiteStatistics(Owner.Id),
                                                  TopTenViewed = _reportingRepository.GetTopTenViewedPhotos(Owner.Id)
                                              };

            dashboardView = SetAuthorizationAndUrlService(dashboardView);
            return View(dashboardView, crumbs);
        }

        /// <summary>
        /// Gets the comment counts.
        /// </summary>
        /// <param name="commentStatuses">The comment statuses.</param>
        /// <returns></returns>
        private List<Statistic> GetCommentCounts(IEnumerable<CommentStatus> commentStatuses)
        {
            return (from status in commentStatuses
                    let comments = _commentRepository.RetrieveCommentByStatusAndUserId(Owner.Id, status)
                    where comments.Count > 0 && status != CommentStatus.Deleted
                    select new Statistic { Count = comments.Count, Name = RenderCommentName(status)}).ToList();
        }

        /// <summary>
        /// Renders the name of the comment.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private static string RenderCommentName(CommentStatus status)
        {
            return (from c in CommentColors(status)
                   where c.Item1 == status
                   select c.Item2).FirstOrDefault();
        }

        /// <summary>
        /// Comments the colors.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private static IEnumerable<Tuple<CommentStatus, string>> CommentColors(CommentStatus status)
        {
            const string format = "<label>{0}</label>";

            return new List<Tuple<CommentStatus, string>>
                       {
                           new Tuple<CommentStatus, string>(CommentStatus.Approved, string.Format(format,  status)),
                           new Tuple<CommentStatus, string>(CommentStatus.Pending, string.Format(format, status)),
                           new Tuple<CommentStatus, string>(CommentStatus.Spam, string.Format(format, status)),
                       };
        }

        /// <summary>
        /// Renders the recent friends.
        /// </summary>
        /// <param name="retrieveLastSevenDaysOfFriendsActivity">The retrieve last seven days of friends activity.</param>
        /// <returns></returns>
        private string RenderRecentFriends(IEnumerable<RecentActivityByUser> retrieveLastSevenDaysOfFriendsActivity)
        {

            var groupedByUserId = from u in retrieveLastSevenDaysOfFriendsActivity
                                  group u by u.UserId into g
                                  select new { Activity = g };

            StringBuilder builder = new StringBuilder();

            foreach (var group in groupedByUserId)
            {
                List<RecentActivityByUser> activityByUsers = group.Activity.ToList();
                string result =  _statisticService.RenderActivity(activityByUsers);

                if (!string.IsNullOrEmpty(result))
                {
                    int userId = activityByUsers[0].UserId;
                    Domain.Model.User user = _userRepository.RetrieveByPrimaryKey(userId);

                    string userHtml = string.Format(@"<ul class=""recentfriendactivity"">
                                  <li>{0}
                                    <ul>{1}</ul>
                                  </li>
                                </ul>", user.DisplayName, result);

                    builder.AppendLine(userHtml);
                }

            }
            return builder.ToString();
        }

        /// <summary>
        /// Homes the bread crumb.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> HomeBreadCrumb()
        {
            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                      {
                                                          {"home", UrlService.UserRoot()},
                                                          {"dashboard", UrlService.UserUrl( "dashboard")}
                                                      };

            return crumbs;
        }

    }
}