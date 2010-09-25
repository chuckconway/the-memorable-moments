using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments
{
    public interface IReportingRepository
    {
        /// <summary>
        /// Sites the statistics.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Statistic> SiteStatistics(int userId);

        /// <summary>
        /// Gets the top ten viewed photos.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<MediaWithViewCount> GetTopTenViewedPhotos(int userId);
    }
}