using System;
using System.Drawing;
using System.IO;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public interface IQueueFileService
    {

        /// <summary>
        /// Queues the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="batchId">The batch id.</param>
        /// <param name="orginal">The orginal bytes.</param>
        /// <param name="stream">The stream.</param>
        void QueueFile(string fileName, int userId, Guid batchId, byte[] orginal, Stream stream);

        /// <summary>
        /// Gets the bitmap.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        Bitmap GetBitmap(byte[] bytes);

        /// <summary>
        /// Adds the file to queue.
        /// </summary>
        /// <param name="orginalFile">The orginal file.</param>
        /// <param name="mediaId">The media id.</param>
        /// <param name="file">The file.</param>
        void AddFileToQueue(byte[] orginalFile, int mediaId, MediaFile file);
    }
}