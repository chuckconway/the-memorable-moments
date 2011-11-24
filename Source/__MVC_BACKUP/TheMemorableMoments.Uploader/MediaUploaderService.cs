using System.Collections.Generic;
using System.Linq;
using TheMemorableMoments.Domain.Model.Uploader;

namespace TheMemorableMoments.Uploader
{
    public class MediaUploaderService
    {
        private readonly IUploaderMediaRepository _uploader = DependencyInjection.Resolve<IUploaderMediaRepository>();

        /// <summary>
        /// Runs the specified val.
        /// </summary>
        /// <param name="val">The val.</param>
        public void Run(object val)
        {
            List<UploaderMedia> media = _uploader.GetQueuedItemsAndSetItemsToPending();

            foreach (TheUploader uploader in media.Select(uploaderMedia => new TheUploader(uploaderMedia)))
            {
                PhotoSizer sizer = new PhotoSizer(uploader.UploaderMedia);
                sizer.ResizeTheImages();
                uploader.Start();
            }
        }
    }
}
