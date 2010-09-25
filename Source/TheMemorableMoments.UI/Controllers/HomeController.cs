using System.Collections.Generic;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers
{
    public class HomeController : BaseController 
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IPersistentCollectionService _persistentCollectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        public HomeController(IMediaRepository mediaRepository, IPersistentCollectionService persistentCollectionService)
        {
            _mediaRepository = mediaRepository;
            _persistentCollectionService = persistentCollectionService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Media> mediae = _mediaRepository.RetrieveRandom90Photos();
            _persistentCollectionService.SetBackUrl("", SiteCookie);
            
            return View(new HomeView { Media = mediae, UrlService = UserUrlService.GetInstance() });
        }

        /// <summary>
        /// Gets the months.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonths()
        {
            var months = EditPhotoHelper.GetMonths();
            return Json(months);
        }

        /// <summary>
        /// Gets the days.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDays()
        {
            var days = EditPhotoHelper.GetDays();
            return Json(days);
        }

        /// <summary>
        /// Gets the visibility.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetVisibility()
        {
            var visibility = EditPhotoHelper.GetVisibility();
            return Json(visibility);
        }

    }
}
