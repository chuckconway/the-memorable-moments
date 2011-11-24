using System;
using System.Linq;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Uploader;
using TheMemorableMoments.Infrastructure.Repositories.Uploader;

namespace TheMemorableMoments.Domain.Services
{
    public class MediaBatchService : IMediaBatchService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaQueueRepository _mediaQueueRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUploaderMediaRepository _uploaderMediaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaBatchService"/> class.
        /// </summary>
        /// <param name="albumRepository">The album repository.</param>
        /// <param name="locationRepository">The location repository.</param>
        /// <param name="mediaQueueRepository">The media queue repository.</param>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="uploaderMediaRepository">The uploader media repository.</param>
        public MediaBatchService(IAlbumRepository albumRepository, 
            ILocationRepository locationRepository, 
            IMediaQueueRepository mediaQueueRepository, 
            IMediaRepository mediaRepository, 
            IUploaderMediaRepository uploaderMediaRepository)
        {
            _albumRepository = albumRepository;
            _uploaderMediaRepository = uploaderMediaRepository;
            _mediaRepository = mediaRepository;
            _mediaQueueRepository = mediaQueueRepository;
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Updates the details.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="batchId">The batch id.</param>
        public void UpdateDetails(Media media, Guid batchId)
        {
            UploadBatch batch = _mediaQueueRepository.GetUploadBatch(batchId);
            AddMediaToAlbum(batch, media.MediaId);
            _mediaQueueRepository.InsertBatchIdAndMediaId(batchId, media.MediaId);


            media.Tags = batch.Tags;
            media.Day = (batch.Day ?? media.Day);
            media.Month = (batch.Month ?? media.Month);
            media.Year = (batch.Year ?? media.Year);
            media.Status = batch.MediaStatus.ParseEnum<MediaStatus>();
            _mediaRepository.Save(media);

            _uploaderMediaRepository.UpdateMediaStatus(media.MediaId, MediaUploadState.Queued);
            SaveLocation(batch, media.Owner.UserId);
        }

        /// <summary>
        /// Saves the location.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="userId">The user id.</param>
        private void SaveLocation(UploadBatch batch, int userId)
        {
            if(!string.IsNullOrEmpty(batch.LocationName) &&
               batch.Latitude.HasValue &&
               batch.Longitude.HasValue &&
               batch.Zoom.HasValue &&
               !string.IsNullOrEmpty(batch.MapTypeId) )
            {
                Location location = new Location
                                        {
                                            LocationName = batch.LocationName,
                                            Latitude = batch.Latitude.GetValueOrDefault(),
                                            Longitude = batch.Longitude.GetValueOrDefault(),
                                            MapTypeId = batch.MapTypeId,
                                            Zoom = batch.Zoom.GetValueOrDefault(),
                                            UserId = userId
                                        };

                _locationRepository.Save(location);
            }
        }

        /// <summary>
        /// Adds the media to album.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="mediaId">The media id.</param>
        private void AddMediaToAlbum(UploadBatch batch, int mediaId)
        {
            if(!string.IsNullOrEmpty(batch.Albums))
            {
                string[] ids = batch.Albums.Split(',');

                foreach (string id in ids.Where(id => !string.IsNullOrEmpty(id)))
                {
                    _albumRepository.AddPhotoToAlbum(Convert.ToInt32(id), mediaId);
                }
            }
        }
    }
}
