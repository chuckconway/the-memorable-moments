namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class MediaViewsRepository : RepositoryBase, IMediaViewsRepository
    {
        /// <summary>
        /// Updates the count.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="userId">The user id.</param>
        public void UpdateCount(int mediaId, int userId)
        {
            database.NonQuery("MediaViewed_IncrementMediaCount", new {mediaId, userId });
        }
    }
}
