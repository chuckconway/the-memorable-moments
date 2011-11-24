using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Uploader;
using TheMemorableMoments.UI.Models.Views.Upload;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.User
{
    [Authorize]
    public class UploadController : DashboardBaseController 
    {
        private readonly IMediaQueueRepository _mediaQueueRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IPersistentCollectionService _persistentCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadController"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="mediaQueueRepository">The media queue repository.</param>
        /// <param name="collectionRepository">The collection repository.</param>
        /// <param name="persistentCollection">The persistent collection.</param>
        public UploadController(IMediaRepository mediaRepository, 
            IMediaQueueRepository mediaQueueRepository, 
            IPersistentCollectionRepository collectionRepository, 
            IPersistentCollectionService persistentCollection)
        {
            _mediaQueueRepository = mediaQueueRepository;
            _persistentCollection = persistentCollection;
            _mediaRepository = mediaRepository;
        }
        
        /// <summary>
        /// Loads the Default View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IDictionary<string, string> crumbs = GetHomeBreadBrumb();
            crumbs.Add("upload", UrlService.UserUrl("upload"));
            UserUploadView view = ModelFactory<UserUploadView>(new {BatchId = Guid.NewGuid()});
            return View(view, crumbs);
        }
        
        /// <summary>
        /// Gets the home bread brumb.
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetHomeBreadBrumb()
        {
            return new Dictionary<string, string> { { "home", UrlService.UserRoot() } };
        }

        /// <summary>
        /// Saves the photo details.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePhotoDetails(UploadPhotoDetails details)
        {
            UploadBatch batch = Mapper.Map<UploadPhotoDetails, UploadBatch>(details);
            _mediaQueueRepository.InsertBatch(batch);

            return Content("1");
        }

        /// <summary>
        /// Creates the batch id.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateBatchId()
        {
            return Json(new { id = Guid.NewGuid().ToString()});
        }
        
        /// <summary>
        /// Edits the photo details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddDetails(string id)
        {
            IDictionary<string, string> crumbs = GetHomeBreadBrumb();
            crumbs.Add("upload", UrlService.UserUrl("upload"));
            crumbs.Add("add details", UrlService.UserUrl(string.Format("adddetails/{0}", HttpUtility.HtmlEncode(id))));

            Guid batchId = new Guid(id);
            List<LoadedMedia> media = _mediaQueueRepository.GetLoadedMedia(batchId);
            int photoCount = _mediaQueueRepository.GetBatchCount(batchId);

            string persistentKey = _persistentCollection.Set(string.Format("AddDetails_{0}_{1}", Authorization.Owner.Id, batchId), media.ConvertAll(o => (Media)o), Persistence.Temporary);
            string mediaKeys = _persistentCollection.Get(persistentKey);

            AddDetailsView detailsView = ModelFactory<AddDetailsView>(new { mediaKeys, persistentKey, photoCount, batchId }); 
            return View(detailsView, crumbs);
        }

        /// <summary>
        /// Gets the media.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public JsonResult Details(int id)
        {
            Media media = _mediaRepository.RetrieveByPrimaryKeyAndUserId(id, Owner.Id);
            return Json(
                new
                    {
                        title = media.Title, 
                        description = media.Description,
                        year = media.Year,
                        story = media.Description,
                        day = media.Day,
                        month = media.Month,
                        tags = media.Tags,
                        mediaStatus = media.Status.ToString()

                    }, "application/json", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the detail.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
       public JsonResult GetDetails(Guid id)
        {
            List<LoadedMedia> media = _mediaQueueRepository.GetLoadedMedia(id);
            return Json(media.ConvertAll(o => new LoadedPhotos {Id = o.MediaId, 
                Path = UrlService.CreateImageUrl(o.GetImageByPhotoType(PhotoType.Thumbnail).FilePath),
                Rel = UrlService.CreateImageUrl(o.GetImageByPhotoType(PhotoType.Websize).FilePath)
            }));
        }

    }

    public class LoadedPhotos
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the rel.
        /// </summary>
        /// <value>The rel.</value>
        public string Rel { get; set; } 
    }
}
