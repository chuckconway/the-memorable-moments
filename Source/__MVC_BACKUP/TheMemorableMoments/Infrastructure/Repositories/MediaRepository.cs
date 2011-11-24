using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Chucksoft.Core.Extensions;
using Hypersonic;
using System.Data.Common;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.Infrastructure.Repositories.Services;

namespace TheMemorableMoments.Infrastructure.Repositories
{
	public class MediaRepository : RepositoryBase, IMediaRepository
	{
		private readonly IMediaFileRepository _mediaFileRepository;
		private readonly ITagRepository _tagRepository;
		private readonly IMediaFileHydrationService _hydrationService;

		/// <summary>
		/// Initializes a new instance of the <see cref="MediaRepository"/> class.
		/// </summary>
		/// <param name="mediaFileRepository">The media file repository.</param>
		/// <param name="tagRepository">The tag repository.</param>
		/// <param name="hydrationService">The hydration service.</param>
		public MediaRepository(IMediaFileRepository mediaFileRepository, ITagRepository tagRepository, IMediaFileHydrationService hydrationService)
		{
			_mediaFileRepository = mediaFileRepository;
			_hydrationService = hydrationService;
			_tagRepository = tagRepository;
		}

		/// <summary>
		/// Saves the specified media.
		/// </summary>
		/// <param name="media">The media.</param>
		/// <returns></returns>
		public int Save(Media media)
		{
		    int id = media.MediaId > 0 ? Update(media) : Insert(media);
		    return id;
		}

	    /// <summary>
		/// Updates the status.
		/// </summary>
		/// <param name="mediaId">The media id.</param>
		/// <param name="userId">The user id.</param>
		/// <param name="mediaStatus">The media status.</param>
	   public void UpdateStatus(int[] mediaId, int userId, MediaStatus mediaStatus)
	   {
		   foreach (int id in mediaId)
		   {
			   UpdateStatus(id, userId, mediaStatus);
		   }     
	   }

		/// <summary>
		/// Updates the status.
		/// </summary>
		/// <param name="mediaId">The media id.</param>
		/// <param name="userId">The user id.</param>
		/// <param name="mediaStatus">The media status.</param>
		public void UpdateStatus(int mediaId, int userId, MediaStatus mediaStatus)
		{
			database.NonQuery("Media_UpdateStatus", new{mediaId,userId, status = mediaStatus.ToString()});
		}

		/// <summary>
		/// Inserts Media into the Medias Table
		/// </summary>
		/// <param name="media">A new populated media.</param>
		/// <returns>Insert Count</returns>
		private int Insert(Media media)
		{
			DbParameter outParameter = database.MakeParameter("@Identity", 0, ParameterDirection.Output);
			List<DbParameter> parameters = new List<DbParameter> 
			{
					database.MakeParameter("@Title",media.Title),
					database.MakeParameter("@Description",media.Description),
					database.MakeParameter("@MediaMonth",media.Month),
					database.MakeParameter("@MediaYear",media.Year),
					database.MakeParameter("@MediaDay",media.Day),
					database.MakeParameter("@UserId",media.Owner.UserId),
					database.MakeParameter("@Status",media.Status.ToString()),
					database.MakeParameter("@Tags",media.Tags),
					outParameter
			};

			database.NonQuery("Media_Insert", parameters);
			int id = Convert.ToInt32(outParameter.Value);

			foreach (MediaFile file in media.MediaFiles)
			{
				file.MediaId = id;
				_mediaFileRepository.Save(file);
			}

			TagCollection interalTagCollection = new TagCollection(media.Tags);
			foreach (Tag tag in interalTagCollection.Tags)
			{
				_tagRepository.InsertTagWithMediaId(tag, id);
			}

		    return id;
		}

		/// <summary>
		/// Retrieves the photos that do not include photos from album id and user id.
		/// </summary>
		/// <param name="albumId">The album id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserId(int albumId, int userId)
		{
			List<Media> media = database.PopulateCollection("Media_RetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserID", new { albumId, userId }, database.AutoPopulate<Media>);
			_hydrationService.Populate(media);

			return media;
		}

		/// <summary>
		/// Retrieves the photos that do not include photos from album id and user id.
		/// </summary>
		/// <param name="albumId">The album id.</param>
		/// <param name="userId">The user id.</param>
		/// <param name="search">The search.</param>
		/// <returns></returns>
		public List<Media> SearchPhotosThatDoNotIncludePhotosByAlbumId(int albumId, int userId, string search)
		{
			List<Media> media = database.PopulateCollection("Media_SearchPhotosThatDoNotIncludePhotosByAlbumId", new { albumId, userId, search }, database.AutoPopulate<Media>);
			_hydrationService.Populate(media);

			return media;
		}

		/// <summary>
		/// Retrieves the media grouped by tags.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<MediaGroupedByTag> RetrieveMediaGroupedByTags(int userId)
		{
			List<Tag> tags = _tagRepository.RetrieveTagsByUserId(userId);
			List<MediaGroupedByTag> mediaGroupedByTags = GetMediaGroupedByTags(tags, userId);
			return mediaGroupedByTags;
		}

		/// <summary>
		/// Retrieves the media grouped by tags.
		/// </summary>
		/// <param name="tags">The tags.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<MediaGroupedByTag> RetrieveMediaGroupedByTagsAndUserId(List<Tag> tags, int userId)
		{
			List<MediaGroupedByTag> mediaGroupedByTags = GetMediaGroupedByTags(tags, userId);
			return mediaGroupedByTags;
		}

		/// <summary>
		/// Gets the media grouped by tags.
		/// </summary>
		/// <param name="tags">The tags.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		private List<MediaGroupedByTag> GetMediaGroupedByTags(IEnumerable<Tag> tags, int userId)
		{
			return tags.Select(tag => new MediaGroupedByTag(tag.TagText)
										  {
											  Id = tag.Id,
											  Media = RetrieveByTagIdAndUserId(tag.Id, userId)
										  }).ToList();
		}

		/// <summary>
		/// Retrieves the photos that do not include photos from album id and user id.
		/// </summary>
		/// <param name="albumId">The album id.</param>
		/// <param name="userId">The user id.</param>
		/// <param name="tagCollection">The tag collection.</param>
		/// <returns></returns>
		public List<Media> SearchTagsForPhotosThatDoNotIncludePhotosFromAlbumIdAndUserId(int albumId, int userId, TagCollection tagCollection)
		{
            DataTable tags = new DataTable();
            tags.Columns.Add("tag");

            foreach (Tag tag in tagCollection.Tags)
            {
                DataRow row = tags.NewRow();
                row["tag"] = tag.TagText;
                tags.Rows.Add(row);
            }

		    List<Media> collection = database.PopulateCollection("Media_SearchTagsForPhotosThatDoNotIncludePhotosFromAlbumIdAndUserID", new { albumId, userId, tags }, database.AutoPopulate<Media>);
            _hydrationService.Populate(collection);
		    return collection;
		}

		
		/// <summary>
		/// Updates the Media table by the primary key, if the Media is dirty then an update will occur
		/// </summary>
		/// <param name="media">a populated media</param>
		/// <returns>update count</returns>
		private int Update(Media media)
		{
            List<DbParameter> parameters = new List<DbParameter> 
                {
                    database.MakeParameter("@MediaId",media.MediaId),
                    database.MakeParameter("@Title",media.Title),
                    database.MakeParameter("@Description",media.Description),
                    database.MakeParameter("@MediaMonth",media.Month),
                    database.MakeParameter("@MediaYear",media.Year),
                    database.MakeParameter("@MediaDay",media.Day),
                    database.MakeParameter("@UserId",media.Owner.UserId),
                    database.MakeParameter("@Status",media.Status.ToString()),
                    database.MakeParameter("@Tags",media.Tags),
                    database.MakeParameter("@LocationName",media.Location.LocationName),
                    database.MakeParameter("@Latitude",media.Location.Latitude),
                    database.MakeParameter("@Longitude",media.Location.Longitude),
                    database.MakeParameter("@Zoom",media.Location.Zoom),
                    database.MakeParameter("@MapTypeId",media.Location.MapTypeId)
                };

            database.NonQuery("Media_Update", parameters);
			_tagRepository.Update(media.Tags, media.MediaId);

		    return 0;
		}

		/// <summary>
		/// Delete a Media by the primary key
		/// </summary>
		/// <param name="mediaId">The media id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public int Delete(int mediaId, int userId)
		{
			return database.NonQuery("Media_Delete", new{mediaId, userId});
		}

		/// <summary>
		/// Retrieves all.
		/// </summary>
		/// <returns></returns>
		public List<Media> RetrieveAll()
		{
			return database.PopulateCollection("Media_RetrieveAll", database.AutoPopulate<Media>);
		}

		/// <summary>
		/// Retrieves the by primary key.
		/// </summary>
		/// <param name="mediaId">The media id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public Media RetrieveByPrimaryKeyAndUserId(int mediaId, int userId)
		{
			Media media = database.PopulateItem("Media_SelectByPrimaryKeyAndUserId", new { mediaId, userId }, database.AutoPopulate<Media>);
			_hydrationService.Populate(new List<Media> { media });

			return media;
		}
		

		/// <summary>
		/// Retreieves the by user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetreieveByUserId(int userId)
		{
			List<Media> media = database.PopulateCollection("Media_RetrieveByUserId", new { userId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);

			return media;
		}

		/// <summary>
		/// Retrieves the by media ids.
		/// </summary>
		/// <param name="mediaIds">The media ids.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrieveByMediaIds(int[] mediaIds, int userId)
		{
			List<Media> mediae = new List<Media>();

			if (mediaIds != null)
			{
			   mediae = mediaIds.Select(mediaId => RetrieveByPrimaryKeyAndUserId(mediaId, userId)).ToList();
			}

			return mediae;
		}

		/// <summary>
		/// Retrieves the random 50 photos.
		/// </summary>
		/// <returns></returns>
		public List<Media> RetrieveRandom90Photos()
		{
			List<Media> media = database.PopulateCollection("Media_RetrieveRandom90Photos", database.AutoPopulate<Media>);
            _hydrationService.Populate(media);
			return media;
		}

		/// <summary>
		/// Retrieves the random50 photos.
		/// </summary>
		/// <returns></returns>
		public List<Media> Retrieve25RecentPhotosByUserId(int userId)
		{
			List<Media> media = database.PopulateCollection("Media_Retrieve25RecentPhotosByUserId", new { userId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);
			return media;
		}

		/// <summary>
		/// Retrieves the by tag name and user id.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrieveByTagNameAndUserId(string tagName, int userId)
		{
			List<Media> media = database.PopulateCollection("Media_RetrieveByTagNameAndUserId", new { userId, tagName }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);
			return media;
		}

		/// <summary>
		/// Retrieves the by tag name and user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <param name="albumId">The album id.</param>
		/// <returns></returns>
		public List<Media> RetrieveByAlbumIdAndUserId(int userId, int albumId)
		{
			List<Media> media = database.PopulateCollection("Media_RetrieveByAlbumIdAndUserId", new { userId, albumId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);
			return media;
		}
		
		/// <summary>
		/// Searches for the specified text.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <returns></returns>
		public List<Media> Search(string search)
		{
			List<Media> media = database.PopulateCollection("Media_Search", new { search }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);

			return media;
		}

		/// <summary>
		/// Searches for the specified text.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> SearchByTextAndUserId(string search, int userId)
		{
			List<Media> media = database.PopulateCollection("Media_SearchByTextAndUserId", new { search, userId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);

			return media;
		}

		/// <summary>
		/// Retrieves the private.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrievePublic(int userId)
		{
			return RetrieveByStatus(MediaStatus.Public, userId);
		}

		/// <summary>
		/// Retrieves the private.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrievePrivate(int userId)
		{
			return RetrieveByStatus(MediaStatus.Private, userId);
		}

        /// <summary>
        /// Gets the media with album position by album id.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<MediaWithAlbumPosition> GetMediaWithAlbumPositionByAlbumId(int albumId, int userId)
        {
            List<MediaWithAlbumPosition> albumPositions = database.PopulateCollection("Media_RetrieveByAlbumIdAndUserId", new { albumId, userId }, database.AutoPopulate<MediaWithAlbumPosition>);
            _hydrationService.Populate(albumPositions);

            return albumPositions;
        }

	    /// <summary>
		/// Retrieves the in network.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrieveInNetwork(int userId)
		{
			return RetrieveByStatus(MediaStatus.InNetwork, userId);
		}

		/// <summary>
		/// Gets the media status count by user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<StatusCount<MediaStatus>> GetMediaStatusCountByUserId(int userId)
		{
			Func<INullableReader,StatusCount<MediaStatus>> populate = o => new StatusCount<MediaStatus> { Status = o.GetNullableString("Status").ParseEnum<MediaStatus>(), Count = o.GetInt32("StatusCount")};
			List<StatusCount<MediaStatus>> count = database.PopulateCollection("Media_RetreiveMediaStatusesCountByUserId", new { userId }, populate);

			return count;
		}
		/// <summary>
		/// Retrieves the by status.
		/// </summary>
		/// <param name="status">The status.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrieveByStatus(MediaStatus status, int userId)
		{
			List<Media> media = database.PopulateCollection("Media_RetrieveByStatus", new { status = status.ToString(), userId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);

			return media;
		}

		/// <summary>
		/// Retrieves the by tag id and user id.
		/// </summary>
		/// <param name="tagId">The tag id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Media> RetrieveByTagIdAndUserId(int tagId, int userId)
		{
			List<Media> media = database.PopulateCollection("Media_RetrieveByTagIdAndUserId", new { tagId, userId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);

			return media;
		}

		/// <summary>
		/// Retrieves the media by batch id.
		/// </summary>
		/// <param name="batchId">The batch id.</param>
		/// <returns></returns>
		public List<Media> RetrieveMediaByBatchId(Guid batchId)
		{
			List<Media> media = database.PopulateCollection("MediaUploadBatch_RetrieveMediaByBatchId", new { batchId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);

			return media;
		}

        /// <summary>
        /// Removes the tag.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="tagName">Name of the tag.</param>
        public void RemoveTag(int mediaId, string tagName)
        {
            database.NonQuery("Media_RemoveTag", new {mediaId, tagName});
        }

		/// <summary>
		/// Gets the random image by album id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <param name="albumId">The album id.</param>
		/// <returns></returns>
		public Media GetRandomImageByAlbumId(int userId, int albumId)
		{
			Media media = database.PopulateItem("Media_RetrieveRandomByAlbumId", new { albumId, userId }, database.AutoPopulate<Media>);
			
			if(media != null)
			{
				media.MediaFiles = _mediaFileRepository.RetrieveByMediaId(media.MediaId);
			}

			return media;
		}
	}
}
