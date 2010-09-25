using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments
{
    public interface IMediaRepository
    {
        /// <summary>
        /// Saves the specified media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        int Save(Media media);

        /// <summary>
        /// Delete a Media by the primary key
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        int Delete(int mediaId, int userId);

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        List<Media> RetrieveAll();

        /// <summary>
        /// Retreieves the by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetreieveByUserId(int userId);

        /// <summary>
        /// Retrieves the random50 photos.
        /// </summary>
        /// <returns></returns>
        List<Media> RetrieveRandom90Photos();

        /// <summary>
        /// Searches for the specified text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        List<Media> Search(string searchText);

        /// <summary>
        /// Retrieves the random 84 photos by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> Retrieve25RecentPhotosByUserId(int userId);

        /// <summary>
        /// Searches the by text and user id.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> SearchByTextAndUserId(string searchText, int userId);

        /// <summary>
        /// Retrieves the by tag name and user id.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrieveByTagNameAndUserId(string tagName, int userId);

        /// <summary>
        /// Retrieves the by primary key and user id.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        Media RetrieveByPrimaryKeyAndUserId(int key, int userId);
        
        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="mediaStatus">The media status.</param>
        void UpdateStatus(int mediaId, int userId, MediaStatus mediaStatus);

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="mediaStatus">The media status.</param>
        void UpdateStatus(int[] mediaId, int userId, MediaStatus mediaStatus);
        
        /// <summary>
        /// Retrieves the private.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrievePrivate(int userId);

        /// <summary>
        /// Retrieves the in network.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrieveInNetwork(int userId);

        /// <summary>
        /// Gets the media status count by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<StatusCount<MediaStatus>> GetMediaStatusCountByUserId(int userId);

        /// <summary>
        /// Retrieves the public.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrievePublic(int userId);

        /// <summary>
        /// Gets the random image by album id.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Media GetRandomImageByAlbumId(int userid, int id);

        /// <summary>
        /// Retrieves the by album id and user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        List<Media> RetrieveByAlbumIdAndUserId(int userId, int albumId);

        /// <summary>
        /// Retrieves the photos that do not include photos from album id and user id.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserId(int albumId, int userId);


        /// <summary>
        /// Searches the photos that do not include photos from album id and user id.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        List<Media> SearchPhotosThatDoNotIncludePhotosByAlbumId(int albumId, int userId, string searchText);

        /// <summary>
        /// Searches the tags for photos that do not include photos from album id and user id.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="tagCollection">The tag collection.</param>
        /// <returns></returns>
        List<Media> SearchTagsForPhotosThatDoNotIncludePhotosFromAlbumIdAndUserId(int albumId, int userId, TagCollection tagCollection);

        /// <summary>
        /// Retrieves the by tag id and user id.
        /// </summary>
        /// <param name="tagId">The tag id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrieveByTagIdAndUserId(int tagId, int userId);

        /// <summary>
        /// Retrieves the media grouped by tags.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<MediaGroupedByTag> RetrieveMediaGroupedByTags(int userId);

        /// <summary>
        /// Retrieves the media grouped by tag id and user id.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<MediaGroupedByTag> RetrieveMediaGroupedByTagsAndUserId(List<Tag> tags, int userId);

        /// <summary>
        /// Retrieves the media by batch id.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns></returns>
        List<Media> RetrieveMediaByBatchId(Guid batchId);

        /// <summary>
        /// Retrieves the by media ids.
        /// </summary>
        /// <param name="mediaIds">The media ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Media> RetrieveByMediaIds(int[] mediaIds, int userId);

        /// <summary>
        /// Removes the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The id.</param>
        void RemoveTag(int id, string tag);

        /// <summary>
        /// Gets the media with album position by album id.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<MediaWithAlbumPosition> GetMediaWithAlbumPositionByAlbumId(int albumId, int userId);
    }
}