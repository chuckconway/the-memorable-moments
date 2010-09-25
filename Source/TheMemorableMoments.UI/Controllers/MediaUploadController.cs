using System;
using System.Web;
using System.Web.Mvc;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Services;
using TheMemorableMoments.UI.Controllers.User;
using TheMemorableMoments.UI.Web.Controllers;

namespace TheMemorableMoments.UI.Controllers
{
    public class MediaUploadController : AnonymousController
    {
        private readonly IQueueFileService _queueFileService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadController"/> class.
        /// </summary>
        /// <param name="queueFileService">The queue file service.</param>
        public MediaUploadController(IQueueFileService queueFileService)
        {
            _queueFileService = queueFileService;
        }

        ///// <summary>
        ///// Photoses this instance.
        ///// </summary>
        ///// <param name="id">The id.</param>
        ///// <param name="batch">The batch.</param>
        ///// <param name="t">The tag.</param>
        ///// <returns></returns>
        //public ActionResult Photos(int id, string batch, string t)
        //{
        //    Guid batchId = new Guid(batch);
        //    HttpPostedFileBase file = Request.Files["Filedata"];
        //    if (file != null)
        //    {
        //        //add untagged to all photos that don't have tags on up load.
        //        t = (string.IsNullOrEmpty(t) ? "untagged" : t);
        //        byte[] orginal = file.InputStream.ReadToEnd();
        //        string fileName = file.FileName;

        //        _queueFileService.QueueFile(t, fileName, id, batchId, orginal);
        //    }

        //    return Content("1");
        //}


    }
}

