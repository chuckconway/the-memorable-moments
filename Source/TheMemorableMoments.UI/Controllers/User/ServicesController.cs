using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Mvc;
using Chucksoft.Core.Drawing;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Services;
using TheMemorableMoments.UI.Web.Controllers;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class ServicesController : AnonymousController
    {
        private readonly IImageService _imageService;
        private readonly IImageConverter _imageConverter;
        private readonly IQueueFileService _queueFileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesController"/> class.
        /// </summary>
        /// <param name="imageService">The image service.</param>
        /// <param name="imageConverter">The image converter.</param>
        /// <param name="queueFileService">The queue file service.</param>
        public ServicesController(IImageService imageService, 
                                  IImageConverter imageConverter, IQueueFileService queueFileService) 
        {
            _imageConverter = imageConverter;
            _queueFileService = queueFileService;
            _imageService = imageService;
        }

        /// <summary>
        /// Grayscales the specified id.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult Grayscale(Media media)
        {
            MediaFile thumbnail = media.GetImageByPhotoType(PhotoType.Thumbnail);
            byte[] fileBytes = _imageService.GetFileBytes(thumbnail, Owner);
            string contentType = _imageService.GetContentType(thumbnail);
            Bitmap bitmap = _imageConverter.ConvertByteArrayToBitmap(fileBytes);
            Bitmap grayScale = _imageConverter.ConvertToGrayScale(bitmap);

            byte[] grayScaleBytes = _imageConverter.ConvertBitmaptoBytes(grayScale, ImageFormat.Jpeg);

            bitmap.Dispose();
            grayScale.Dispose();

            return File(grayScaleBytes, contentType);
        }

        /// <summary>
        /// Photoses this instance.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Photos(string id)
        {
            Guid batchId = new Guid(id);
            HttpPostedFileBase file = Request.Files["Filedata"];
            if (file != null)
            {
                byte[] orginal = file.InputStream.ReadToEnd();
                string fileName = file.FileName;

                _queueFileService.QueueFile(fileName, Owner.Id, batchId, orginal, file.InputStream);
            }

            return Content("1");
        }
        
    }
}
