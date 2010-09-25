using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Albums;

namespace TheMemorableMoments
{
    public interface IAlbumRepository
    {
        /// <summary>
        /// Delete a Album by the primary key
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        int Delete(int albumId, int userId);

        /// <summary>
        /// Sets the cover image.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="mediaId">The media id.</param>
        void SetCoverImage(int albumId, int mediaId);

        /// <summary>
        /// Saves the specified album.
        /// </summary>
        /// <param name="album">The album.</param>
        void Save(Album album);

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        List<Album> RetrieveAll();

        /// <summary>
        /// Searches the specified search text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Album> Search(string searchText, int? albumId, int userId);

        /// <summary>
        /// Adds the photo to album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="mediaId">The media id.</param>
        void AddPhotoToAlbum(int albumId, int mediaId);

        /// <summary>
        /// Adds the photos to album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="selectedIds">The selected ids.</param>
        void AddPhotosToAlbum(int albumId, int[] selectedIds);

        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Album RetrieveByPrimaryKey(int key);

        /// <summary>
        /// Retrieves all by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Album> RetrieveAllByUserId(int userId);

        /// <summary>
        /// Retrieves the by user id and primay key.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Album RetrieveByUserIdAndPrimayKey(int userId, int id);

        /// <summary>
        /// Retrieves all by user id and parent id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="parentId">The parent id.</param>
        /// <returns></returns>
        List<Album> RetrieveAllByUserIdAndParentId(int userId, int? parentId);

        /// <summary>
        /// Retrieves the album hierarchy by album id and user id.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Album> RetrieveAlbumHierarchyByAlbumIdAndUserId(int albumId, int userId);

        /// <summary>
        /// Retrieves the top level albums by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Album> RetrieveTopLevelAlbumsByUserId(int userId);

        /// <summary>
        /// Deletes the photos from albums.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="ids">The ids.</param>
        void DeletePhotosFromAlbum(int albumId, int[] ids);

        /// <summary>
        /// Updates the media position.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="albumId">The album id.</param>
        /// <param name="positions">The positions.</param>
        void UpdateMediaPosition(int userid, int albumId, List<MediaPosition> positions);

        /// <summary>
        /// Removes the media fro album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="mediaId">The media id.</param>
        void RemoveMediaFromAlbum(int albumId, int mediaId);

        /// <summary>
        /// Adds the albums to photo.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="albumIds">The album ids.</param>
        void AddAlbumsToPhoto(int mediaId, int[] albumIds);

    }
}