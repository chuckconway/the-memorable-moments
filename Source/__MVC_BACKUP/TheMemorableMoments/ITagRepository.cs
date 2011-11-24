using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments
{
    public interface ITagRepository
    {
        /// <summary>
        /// Inserts Tag into the Tags Table
        /// </summary>
        /// <param name="tag">A new populated tag.</param>
        /// <returns>Insert Count</returns>
        int Insert(Tag tag);

        /// <summary>
        /// Updates the Tag table by the primary key, if the Tag is dirty then an update will occur
        /// </summary>
        /// <param name="tag">a populated tag</param>
        /// <param name="userId">The user id.</param>
        /// <returns>update count</returns>
        int Update(Tag tag, int userId);

        /// <summary>
        /// Updates the specified tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="mediaId">The media id.</param>
        void Update(string tags, int mediaId);

        /// <summary>
        /// Delete a Tag by the primary key
        /// </summary>
        /// <param name="tag"></param>
        int Delete(Tag tag);

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        List<Tag> RetrieveAll();

        /// <summary>
        /// Retrieves the tags by user id and tag id.
        /// </summary>
        /// <param name="tagId">The tag id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        Tag RetrieveTagsByUserIdAndTagId(int tagId, int userId);

        /// <summary>
        /// Retrieves the tag by name and user id.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        Tag RetrieveTagByNameAndUserId(string tagName, int userId);

        /// <summary>
        /// Inserts the tag with media id.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="mediaId">The media id.</param>
        void InsertTagWithMediaId(Tag tag, int mediaId);

        /// <summary>
        /// Retrieves the tags by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Tag> RetrieveTagsByUserId(int userId);

        /// <summary>
        /// Searches the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        List<Tag> Search(int userId, string text);

        /// <summary>
        /// Gets the related tags.
        /// </summary>
        /// <param name="tagId">The tag id.</param>
        /// <returns></returns>
        List<Tag> GetRelatedTags(int tagId);

        /// <summary>
        /// Gets the related tags by year.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        List<Tag> GetRelatedTagsByYear(int userId, int year);
    }
}