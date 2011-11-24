using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class MediaFileRepository : RepositoryBase, IMediaFileRepository
    {
        /// <summary>
        /// Saves the specified media file.
        /// </summary>
        /// <param name="mediaFile">The media file.</param>
        /// <returns></returns>
        public int Save(MediaFile mediaFile)
        {
            int result = (mediaFile.FileId > 0 ? Update(mediaFile) : Insert(mediaFile));
            return result;
        }

        /// <summary>
        /// Saves the specified media files.
        /// </summary>
        /// <param name="mediaFiles">The media files.</param>
        public void Save(List<MediaFile> mediaFiles)
        {
            mediaFiles.ForEach(x => Save(x));
        }

        /// <summary>
        /// Inserts File into the Files Table
        /// </summary>
        /// <param name="file">A new populated file.</param>
        /// <returns>Insert Count</returns>
        private int Insert(MediaFile file)
        {
            DbParameter outParameter = database.MakeParameter("@Identity",0 ,ParameterDirection.Output);

            List<DbParameter> parameters = new List<DbParameter> 
			{
					database.MakeParameter("@FilePath",file.FilePath),
                    database.MakeParameter("@MediaId",file.MediaId),
					database.MakeParameter("@FileExtension",file.FileExtension),
                    database.MakeParameter("@Size",file.Size),
					database.MakeParameter("@OriginalFileName",file.OriginalFileName),
			        outParameter,
                    database.MakeParameter("@MediaFormat", file.MediaFormat.ToString()),
                    database.MakeParameter("@MediaType", file.PhotoType.ToString()),
                    database.MakeParameter("@Width",file.Width),
                    database.MakeParameter("@Height",file.Height),
			};

            database.NonQuery("File_Insert", parameters);
            return Convert.ToInt32(outParameter.Value);
        }

        /// <summary>
        /// Updates the dimensions.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void UpdateDimension(int fileId, int width, int height)
        {
            database.NonQuery("File_UpdateDimension", new { fileId, width, height });
        }
        
        /// <summary>
        /// Updates the File table by the primary key, if the File is dirty then an update will occur
        /// </summary>
        /// <param name="file">a populated file</param>
        /// <returns>update count</returns>
        private int Update(MediaFile file)
        {
            int updateCount = database.NonQuery("File_Update", new{file.FileId,file.FilePath, file.FileExtension, file.OriginalFileName, file.Size});
            return updateCount;
        }

        /// <summary>
        /// Delete a File by the primary key
        /// </summary>
        /// <param name="file"></param>
        public int Delete(MediaFile file)
        {
            return database.NonQuery("File_Delete", new{file.FileId});
        }

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        public List<MediaFile> RetrieveAll()
        {
            return database.PopulateCollection("File_RetrieveAll", database.AutoPopulate<MediaFile>);
        }

        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <returns></returns>
        public MediaFile RetrieveByPrimaryKey(int fileId)
        {
            return database.PopulateItem("File_RetrieveByPrimaryKey", new { fileId }, database.AutoPopulate<MediaFile>);
        }

        /// <summary>
        /// Retrieves the by media id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public List<MediaFile> RetrieveByMediaId(int mediaId)
        {
            return database.PopulateCollection("File_RetrieveByMediaId ", new{mediaId}, database.AutoPopulate<MediaFile>);
        }

        /// <summary>
        /// Retrieves the by media id.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public List<MediaFile> RetrieveByMediaIds(DataTable ids)
        {
            return database.PopulateCollection("File_RetrieveByMediaIdCollection", new { ids }, database.AutoPopulate<MediaFile>);
        }

    }
}
