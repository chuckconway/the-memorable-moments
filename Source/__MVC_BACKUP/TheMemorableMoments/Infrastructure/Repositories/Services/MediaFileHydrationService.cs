using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Infrastructure.Repositories.Services
{
    public class MediaFileHydrationService : IMediaFileHydrationService
    {
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IBelongsToAlbumRepository _belongsToAlbumRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileHydrationService"/> class.
        /// </summary>
        /// <param name="mediaFileRepository">The media file repository.</param>
        /// <param name="locationRepository">The location repository.</param>
        /// <param name="belongsToAlbumRepository">The belongs to album repository.</param>
        public MediaFileHydrationService(IMediaFileRepository mediaFileRepository, ILocationRepository locationRepository, IBelongsToAlbumRepository belongsToAlbumRepository)
        {
            _mediaFileRepository = mediaFileRepository;
            _belongsToAlbumRepository = belongsToAlbumRepository;
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Populates the files.
        /// </summary>
        /// <param name="media">The media.</param>
        public void Populate<T>(List<T> media) where T : IMediaFiles
        {
            PopulateMediaFiles(media, _mediaFileRepository.RetrieveByMediaIds);
            PopulateLocation(media, _locationRepository.GetLocationsByMediaId);
            PopulateAlbums(media, _belongsToAlbumRepository.RetrieveByMediaIds);
        }

        /// <summary>
        /// Populates the media files.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="mediaFiles">The media files.</param>
        internal static void PopulateLocation<T>(IEnumerable<T> media, Func<DataTable, List<Location>> mediaFiles) where T : IMediaFiles
        {
            DataTable ids = new DataTable();
            ids.Columns.Add("Id", typeof(int));

            foreach (T list in media.Where(list => list != null))
            {
                DataRow dataRow = ids.NewRow();
                dataRow["id"] = list.MediaId;
                ids.Rows.Add(dataRow);
            }

            ids.AcceptChanges();
            HydrateMediaWithLocation(ids, media, mediaFiles);
        }

        /// <summary>
        /// Populates the media files.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="media">The media.</param>
        /// <param name="getAlbums">The get albums.</param>
        internal static void PopulateAlbums<T>(IEnumerable<T> media, Func<DataTable, List<BelongsToAlbum>> getAlbums) where T : IMediaFiles
        {
            DataTable ids = new DataTable();
            ids.Columns.Add("Id", typeof(int));

            foreach (T list in media.Where(list => list != null))
            {
                DataRow dataRow = ids.NewRow();
                dataRow["id"] = list.MediaId;
                ids.Rows.Add(dataRow);
            }

            ids.AcceptChanges();
            HydrateMediaWithAlbums(ids, media, getAlbums);
        }


        /// <summary>
        /// Populates the media files.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="mediaFiles">The media files.</param>
        internal static void PopulateMediaFiles<T>(IEnumerable<T> media, Func<DataTable, List<MediaFile>> mediaFiles) where T : IMediaFiles
        {
            DataTable ids = new DataTable();
            ids.Columns.Add("Id", typeof(int));

            foreach (T list in media.Where(list => list != null))
            {
                DataRow dataRow = ids.NewRow();
                dataRow["id"] = list.MediaId;
                ids.Rows.Add(dataRow);
            }

            ids.AcceptChanges();
            HydrateMediaWithFiles(ids, media, mediaFiles);
        }

        /// <summary>
        /// Hydrates the media with files.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids">The ids.</param>
        /// <param name="media">The media.</param>
        /// <param name="getLocations">The get locations.</param>
        private static void HydrateMediaWithLocation<T>(DataTable ids, IEnumerable<T> media, Func<DataTable, List<Location>> getLocations) where T : IMediaFiles
        {
            List<Location> locations = getLocations(ids);

            foreach (T list in media.Where(list => list != null))
            {
                T populateMedia = list;
                foreach (Location location in locations.Where(file => populateMedia.MediaId == file.MediaId))
                {
                    populateMedia.Location = location;
                }
            }
        }

        /// <summary>
        /// Hydrates the media with files.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids">The ids.</param>
        /// <param name="media">The media.</param>
        /// <param name="getLocations">The get locations.</param>
        private static void HydrateMediaWithAlbums<T>(DataTable ids, IEnumerable<T> media, Func<DataTable, List<BelongsToAlbum>> getLocations) where T : IMediaFiles
        {
           List<BelongsToAlbum> files = getLocations(ids);

            foreach (T list in media.Where(list => list != null))
            {
                T populateMedia = list;
                foreach (BelongsToAlbum file in files.Where(file => populateMedia.MediaId == file.MediaId))
                {
                    if (populateMedia.BelongsToAlbums == null)
                    {
                        populateMedia.BelongsToAlbums = new List<BelongsToAlbum>();
                    }

                    populateMedia.BelongsToAlbums.Add(file);
                }
            }
        }

        /// <summary>
        /// Hydrates the media with files.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="media">The media.</param>
        /// <param name="mediaFiles">The media files.</param>
        private static void HydrateMediaWithFiles<T>(DataTable ids, IEnumerable<T> media, Func<DataTable, List<MediaFile>> mediaFiles) where T : IMediaFiles
        {
            List<MediaFile> files = mediaFiles(ids);

            foreach (T list in media.Where(list => list != null))
            {
                T populateMedia = list;
                foreach (MediaFile file in files.Where(file => populateMedia.MediaId == file.MediaId))
                {
                    if (populateMedia.MediaFiles == null)
                    {
                        populateMedia.MediaFiles = new List<MediaFile>();
                    }

                    populateMedia.MediaFiles.Add(file);
                }
            }
        }
    }
}
