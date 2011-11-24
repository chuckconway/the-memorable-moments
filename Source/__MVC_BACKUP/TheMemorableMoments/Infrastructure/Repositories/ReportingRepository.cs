using System.Collections.Generic;
using Chucksoft.Core.Extensions;
using Hypersonic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Infrastructure.Repositories.Services;

namespace TheMemorableMoments.Infrastructure.Repositories
{
   public class ReportingRepository : RepositoryBase, IReportingRepository
   {
       private readonly IMediaFileHydrationService _hydrationService;

       /// <summary>
       /// Initializes a new instance of the <see cref="ReportingRepository"/> class.
       /// </summary>
       /// <param name="hydrationService">The hydration service.</param>
       public ReportingRepository(IMediaFileHydrationService hydrationService)
       {
           _hydrationService = hydrationService;
       }

       /// <summary>
       /// Gets the top ten viewed photos.
       /// </summary>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public List<MediaWithViewCount> GetTopTenViewedPhotos(int userId)
       {
           List<MediaWithViewCount> mediaWithViewCounts =  database.PopulateCollection("Reporting_Top10MostViewed", new {UserId = userId}, PopulateMediaWithViewCount);
           _hydrationService.Populate(mediaWithViewCounts.ConvertAll(o => (Media)o));

           return mediaWithViewCounts;
       }

        /// <summary>
        /// Sites the statistics.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
       public List<Statistic> SiteStatistics(int userId)
       {
           return database.PopulateCollection("Reporting_SiteStatistics", new {userId}, PopulateStatistic);
       }


       /// <summary>
       /// Populates the media with view count.
       /// </summary>
       /// <param name="reader">The reader.</param>
       /// <returns></returns>
       private static MediaWithViewCount PopulateMediaWithViewCount(INullableReader reader)
       {
           MediaWithViewCount media = new MediaWithViewCount
           {
               MediaId = reader.GetInt32("MediaId"),
               CreateDate = reader.GetDateTime("CreateDate"),
               Title = reader.GetString("Title"),
               Description = reader.GetString("Description"),
               Month = reader.GetNullableInt32("MediaMonth"),
               Year = reader.GetNullableInt32("MediaYear"),
               Day = reader.GetNullableInt32("MediaDay"),
               ViewCount = reader.GetInt32("ViewCount"),
               Owner = new Owner
               {
                   UserId = reader.GetInt32("UserId"),
                   Username = reader.GetString("Username"),
                   FirstName = reader.GetString("FirstName"),
                   LastName = reader.GetString("LastName"),
               },
               Status = reader.GetString("Status").ParseEnum<MediaStatus>(),
               Tags = reader.GetString("Tags"),
               CommentCount = reader.GetInt32("CommentCount")
           };

           return media;
       }

       /// <summary>
       /// Populates the statistic.
       /// </summary>
       /// <param name="reader">The reader.</param>
       /// <returns></returns>
       private static Statistic PopulateStatistic(INullableReader reader)
       {
           Statistic statistic = new Statistic {Count = reader.GetInt32("Count"), Name = reader.GetString("Name")};
           return statistic;
       }
    }
}
