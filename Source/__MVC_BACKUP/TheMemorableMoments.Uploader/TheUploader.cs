using System;
using System.Collections.Generic;
using System.IO;
using Chucksoft.Core.Instrumentation;
using Chucksoft.Storage;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Uploader;

using TheMemorableMoments.Infrastructure.Repositories.Uploader;

namespace TheMemorableMoments.Uploader
{
    public class TheUploader
    {
        private readonly IUploaderMediaRepository _uploaderMediaRepository = DependencyInjection.Resolve<IUploaderMediaRepository>();
        readonly IMediaQueueRepository _mediaQueueRepository = DependencyInjection.Resolve<IMediaQueueRepository>();
        readonly IUserRepository _userRepository = DependencyInjection.Resolve<IUserRepository>();
        private readonly IStorage _fileService = DependencyInjection.Resolve<IStorage>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TheUploader"/> class.
        /// </summary>
        /// <param name="uploaderMedia">The uploader media.</param>
        public TheUploader(UploaderMedia uploaderMedia)
        {
            UploaderMedia = uploaderMedia;
        }

        /// <summary>
        /// Gets the uploader media.
        /// </summary>
        /// <value>The uploader media.</value>
        public UploaderMedia UploaderMedia { get; private set; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            try
            {

                List<MediaQueue> mediaQueue = _mediaQueueRepository.RetrieveQueuedByMediaId(UploaderMedia.Id);
                
                foreach (MediaQueue queue in mediaQueue)
                {
                    User user = _userRepository.RetrieveByPrimaryKey(UploaderMedia.UserId);
                    string ext = Path.GetExtension(queue.Filename);
// ReSharper disable PossibleNullReferenceException
                    ext = ext.Replace(".", "");
// ReSharper restore PossibleNullReferenceException

                    const string contentType = "image/{0}";
                    _fileService.AddFile(user.Username, queue.Filename, string.Format(contentType, ext), queue.MediaBytes);
                }

                //Update the media status from 'Queued' to 'Unpublished'
                _uploaderMediaRepository.UpdateMediaStatus(UploaderMedia.Id, MediaUploadState.Completed);

                //Delete files from the MediaQueue Table
                _mediaQueueRepository.Delete(UploaderMedia.Id);
            }
            catch(Exception exception)
            {
                Logging.Exception(string.Format("TheUploader.Start MediaId:{0}", UploaderMedia.Id), exception, @"C:\Logs\UploadService");
            }
        }
    }
}
