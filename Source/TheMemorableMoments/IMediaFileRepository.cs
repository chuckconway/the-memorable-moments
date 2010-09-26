using System.Collections.Generic;
using System.Data;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments
{
    public interface IMediaFileRepository
    {
        /// <summary>
        /// Saves the specified media file.
        /// </summary>
        /// <param name="mediaFile">The media file.</param>
        /// <returns></returns>
        int Save(MediaFile mediaFile);

        /// <summary>
        /// Delete a File by the primary key
        /// </summary>
        /// <param name="file"></param>
        int Delete(MediaFile file);

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        List<MediaFile> RetrieveAll();

        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        MediaFile RetrieveByPrimaryKey(int key);

        /// <summary>
        /// Retrieves the by media id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        List<MediaFile> RetrieveByMediaId(int mediaId);

        /// <summary>
        /// Retrieves the by media id.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        List<MediaFile> RetrieveByMediaIds(DataTable ids);

        /// <summary>
        /// Saves the specified media files.
        /// </summary>
        /// <param name="mediaFiles">The media files.</param>
        void Save(List<MediaFile> mediaFiles);

        /// <summary>
        /// Updates the dimensions.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void UpdateDimension(int fileId, int width, int height);
    }
}