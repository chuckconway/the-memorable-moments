using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public interface ITrackMediaViewsService
    {
        /// <summary>
        /// Retrieves the by primary key and user id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        Media RetrieveByPrimaryKeyAndUserId(int mediaId, int userId);
    }
}