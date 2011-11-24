using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Recent;

namespace TheMemorableMoments
{
    public interface IRecentRepository
    {
        /// <summary>
        /// Retrieves the last30 days.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<RecentActivity> RetrieveLast30Days(int userId);

        /// <summary>
        /// Retrieves the last seven days of friends activity.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<RecentActivityByUser> RetrieveLastSevenDaysOfFriendsActivity(int userId);

        /// <summary>
        /// Retrieves the recent uploads.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="photoCount">The photo count.</param>
        /// <returns></returns>
        List<RecentUploads> RetrieveRecentUploads(int userId, int photoCount);

        /// <summary>
        /// Retrieves the years for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<YearWithCount> RetrieveYearsForUser(int userId);

        /// <summary>
        /// Retrieves the year by user id.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrieveYearByUserId(int year, int userId);
    }
}