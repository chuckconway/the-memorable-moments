using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.Domain.Services
{
    public interface IUpdateTagsService
    {
        /// <summary>
        /// Updates the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="mediaId">The media id.</param>
        /// <param name="user">The user.</param>
        void UpdateTags(string tags, int mediaId, User user);
    }
}