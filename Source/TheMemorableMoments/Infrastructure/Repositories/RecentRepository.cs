using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Recent;
using TheMemorableMoments.Infrastructure.Repositories.Services;

namespace TheMemorableMoments.Infrastructure.Repositories
{
   public class RecentRepository : RepositoryBase, IRecentRepository
   {
       private readonly IMediaFileHydrationService _hydrationService;
       
       /// <summary>
       /// Initializes a new instance of the <see cref="RecentRepository"/> class.
       /// </summary>
       /// <param name="hydrationService">The hydration service.</param>
       public RecentRepository(IMediaFileHydrationService hydrationService)
       {
           _hydrationService = hydrationService;
       }

       /// <summary>
        /// Retrieves the last30 days.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
       public List<RecentActivity> RetrieveLast30Days(int userId)
       {
           return database.PopulateCollection("RecentActivity_RetrieveOneMonthActivityRollUp", new { UserId = userId }, database.AutoPopulate<RecentActivity>);
       }

       /// <summary>
       /// Retrieves the last weeks friends activity.
       /// </summary>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public List<RecentActivityByUser> RetrieveLastSevenDaysOfFriendsActivity(int userId)
       {
           return database.PopulateCollection("RecentActivity_RetrieveFriendsActivityFromLastWeekByUserId", new { userId }, database.AutoPopulate<RecentActivityByUser>);
       }

       /// <summary>
       /// Retrieves the recent uploads.
       /// </summary>
       /// <param name="userId">The user id.</param>
       /// <param name="photoCount">The photo count.</param>
       /// <returns></returns>
       public List<RecentUploads> RetrieveRecentUploads(int userId, int photoCount)
       {
           List<RecentUploads> collection = database.PopulateCollection("Recent_Uploads", new { userId, photoCount }, database.AutoPopulate<RecentUploads>);
          _hydrationService.Populate(collection.ConvertAll(o => (Media)o));

           return collection;
       }

       /// <summary>
       /// Retrieves the years for user.
       /// </summary>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public List<YearWithCount> RetrieveYearsForUser(int userId)
       {
           List<YearWithCount> collection = database.PopulateCollection("Recent_GetYearsForUser", new { userId }, database.AutoPopulate<YearWithCount>);
           return collection;
       }

       /// <summary>
       /// Retrieves the years for user.
       /// </summary>
       /// <param name="year">The year.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public List<Media> RetrieveYearByUserId(int year, int userId)
       {
           List<Media> collection = database.PopulateCollection("Recent_GetYearByUserId", new { userId, year }, database.AutoPopulate<Media>);
           _hydrationService.Populate(collection);
           return collection;
       }
   }
}
