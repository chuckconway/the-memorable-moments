using System.Collections.Generic;
using System.Data;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class AlbumRepository : RepositoryBase, IAlbumRepository
    {
        private readonly IMediaRepository _mediaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRepository"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        public AlbumRepository(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }
        
        /// <summary>
        /// Deletes the photos from albums.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="ids">The ids.</param>
        public void DeletePhotosFromAlbum(int albumId, int[] ids)
        {
            foreach (int id in ids)
            {
               database.NonQuery("Album_DeletePhotoFromAlbumByPhotoId", new { MediaId = id, albumId});
            }
        }


        /// <summary>
        /// Updates the media position.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="albumId">The album id.</param>
        /// <param name="positions">The positions.</param>
        public void UpdateMediaPosition(int userid, int albumId, List<MediaPosition> positions)
        {
            foreach (MediaPosition mediaPosition in positions)
            {
                database.NonQuery("Album_UpdateMediaPosition", new { userid, albumId, mediaPosition });
            }
        }

        /// <summary>
        /// Retrieves all by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="parentId">The parent id.</param>
        /// <returns></returns>
        public List<Album> RetrieveAllByUserIdAndParentId(int userId, int? parentId)
        {
            List<Album> albums = database.PopulateCollection("Album_RetrieveAllByUserIdAndParentId", new { userId, parentId }, database.AutoPopulate<Album>);
            albums.ForEach(o => o.Media = _mediaRepository.RetrieveByAlbumIdAndUserId(userId, o.AlbumId));

            return albums;
        }

        /// <summary>
        /// Retrieves all by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Album> RetrieveAllByUserId(int userId)
        {
            return GetAlbumsByUserId("Album_RetrieveAllByUserId", userId);
        }

        /// <summary>
        /// Gets the albums by user id.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        private List<Album> GetAlbumsByUserId(string procedureName, int userId)
        {
            List<Album> albums = database.PopulateCollection(procedureName, new { userId }, database.AutoPopulate<Album>);

            albums.ForEach(o =>
                               {
                                   Media media = _mediaRepository.GetRandomImageByAlbumId(userId, o.AlbumId);
                                   if (media != null)
                                   {
                                       o.Media = new List<Media> { media };
                                   }
                               });

            return albums;
        }

        /// <summary>
        /// Removes the media fro album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="mediaId">The media id.</param>
        public void RemoveMediaFromAlbum(int albumId, int mediaId)
        {
            database.NonQuery("Album_DeletePhotoFromAlbumByPhotoId", new { albumId, mediaId });
        }

        /// <summary>
        /// Sets the cover image.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="mediaId">The media id.</param>
        public void SetCoverImage (int albumId, int mediaId)
        {
            database.NonQuery("Album_SetCoverImage", new {albumId, mediaId});
        }

        /// <summary>
        /// Retrieves the top level albums by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Album> RetrieveTopLevelAlbumsByUserId(int userId)
        {
            return GetAlbumsByUserId("Album_RetrieveTopLevelAlbumsByUserId", userId);
        }

        /// <summary>
        /// Delete a Album by the primary key
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Album> Search(string searchText, int? albumId, int userId)
        {
            List<Album> albums = database.PopulateCollection("Album_Search", new { searchText, albumId, userId }, database.AutoPopulate<Album>);
            albums.ForEach(o => o.Media = _mediaRepository.RetrieveByAlbumIdAndUserId(userId, o.AlbumId));

            return albums;
        }

        /// <summary>
        /// Delete a Album by the primary key
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int Delete(int albumId, int userId)
        {
            return database.NonQuery("Album_DeleteAlbumIncludingSubAlbums", new { albumId, userId });
        }

        /// <summary>
        /// Retrieves the by user id and primay key.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        public Album RetrieveByUserIdAndPrimayKey(int userId, int albumId)
        {
            Album album = database.PopulateItem("Album_RetrieveByPrimaryKeyAndUserId", new { userId, albumId }, database.AutoPopulate<Album>);
            if (album != null)
            {
                album.Media = _mediaRepository.RetrieveByAlbumIdAndUserId(userId, albumId);
            }

            return album;
        }

        /// <summary>
        /// Adds the albums to photo.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="albumIds">The album ids.</param>
        public void AddAlbumsToPhoto(int mediaId, int[] albumIds)
        {
            DataTable ids = new DataTable();
            ids.Columns.Add("Id", typeof(int));

            foreach (int id in albumIds)
            {
                DataRow dataRow = ids.NewRow();
                dataRow["id"] = id;
                ids.Rows.Add(dataRow);
            }

            ids.AcceptChanges();
            database.NonQuery("AlbumMedia_UpdateAlbumIdsForMedia", new { ids, mediaId });
        }

        /// <summary>
        /// Adds the photos to album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="selectedIds">The selected ids.</param>
        public void AddPhotosToAlbum(int albumId, int[] selectedIds)
        {
            foreach (int id in selectedIds)
            {
                AddPhotoToAlbum(albumId, id);
            }
        }
        
        /// <summary>
        /// Adds the photo to album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="mediaId">The media id.</param>
        public void AddPhotoToAlbum(int albumId, int mediaId)
        {
            database.NonQuery("AlbumMedia_Insert", new { albumId, mediaId });
        }

        /// <summary>
        /// Retrieves the album hierarchy by album id and user id.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Album> RetrieveAlbumHierarchyByAlbumIdAndUserId(int albumId, int userId)
        {

            return database.PopulateCollection("Album_RetrieveAlbumHierarchyByAlbumIdAndUserId", new{albumId, userId}, database.AutoPopulate<Album>);
        }

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        public List<Album> RetrieveAll()
        {
            return database.PopulateCollection("Album_RetrieveAll", database.AutoPopulate<Album>);
        }
        

        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        public Album RetrieveByPrimaryKey(int albumId)
        {
            return database.PopulateItem("Album_RetrieveByPrimaryKey", new{albumId}, database.AutoPopulate<Album>);
        }

        public void Save(Album album)
        {
            string procedure = ((album.AlbumId < 1) ? "Album_Insert" : "Album_Update");
            database.NonQuery(procedure, album);
        }
    }
}
