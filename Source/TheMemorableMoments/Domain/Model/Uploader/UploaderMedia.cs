using Chucksoft.Core;
using TheMemorableMoments.Infrastructure.Repositories.Uploader;

namespace TheMemorableMoments.Domain.Model.Uploader
{
    public class UploaderMedia :IId<int>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the media status.
        /// </summary>
        /// <value>The media status.</value>
        public MediaUploadState UploadStatus { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }
    }
}