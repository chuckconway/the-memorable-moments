namespace TheMemorableMoments
{
    public interface IMediaViewsRepository
    {
        /// <summary>
        /// Updates the count.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="userId">The user id.</param>
        void UpdateCount(int mediaId, int userId);
    }
}