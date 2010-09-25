using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Models.Views.UserModels;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Feeds;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class UserController : HybridController
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IPopulateSidebarView _sidebarView;
        private readonly IPaginationService<Media> _paginationService;
        private readonly IPersistentCollectionService _persistentCollectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="sidebarView">The sidebar view.</param>
        /// <param name="paginationService">The pagination service.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        public UserController(IMediaRepository mediaRepository, IPopulateSidebarView sidebarView, IPaginationService<Media> paginationService, IPersistentCollectionService persistentCollectionService)
        {
            _mediaRepository = mediaRepository;
            _persistentCollectionService = persistentCollectionService;
            _paginationService = paginationService;
            _sidebarView = sidebarView;
        }

        /// <summary>
        /// Searches the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        public ActionResult Search(string text, string cp)
        {
            List<Media> media = (!string.IsNullOrEmpty(text) ? _mediaRepository.SearchByTextAndUserId(text, Owner.Id) : new List<Media>());
            string encodedText = HttpUtility.HtmlEncode(text);

            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                     {
                                                         {"home", UrlService.UserRoot()},
                                                         {"search", string.Empty},
                                                         { string.Format("'{0}'", encodedText), UrlService.UserUrl(string.Format("search/ '{0}'", encodedText))}
                                                     };

            int page = (string.IsNullOrEmpty(cp) ? 1 : Convert.ToInt32(cp));
            string setKey = string.Empty;

            if (media.Count > 0)
            {
                setKey = _persistentCollectionService.Set(Authorization.Owner.Username + "_user_Search_" + encodedText, media, Persistence.Temporary);
                _persistentCollectionService.SetBackUrl(string.Format("{0}/search?text={1}&cp={2}", Authorization.Owner.Username, encodedText, page), SiteCookie);
            }

            UserSearchView view = ModelFactory<UserSearchView>(new { SearchText = encodedText, TotalResults = media.Count, Pagination = _paginationService, Set = setKey });
            view.Pagination.GeneratePaging(media, page, 20, "?cp={0}&text=" + encodedText);
            _sidebarView.PopulateView(view, Owner);

            return View(view, crumbs);
        }
        
        /// <summary>
        /// Indexes the specified username.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IDictionary<string, string> crumbs = new Dictionary<string, string> { { "home", UrlService.UserRoot() } };
            List<Media> mediae = _mediaRepository.Retrieve25RecentPhotosByUserId(Owner.Id);

            string key = _persistentCollectionService.Set(Authorization.Owner.Username + "_userIndex", mediae, Persistence.Temporary);
            _persistentCollectionService.SetBackUrl(Authorization.Owner.Username, SiteCookie);

            UserHomeView view = ModelFactory<UserHomeView>(new { Media = mediae, Set = key });
            _sidebarView.PopulateView(view, Owner);

            return View(view, crumbs);
        }


        /// <summary>
        /// Serves a Rss feed.
        /// </summary>
        /// <returns></returns>
        public ActionResult Rss()
        {
            List<Media> mediae = _mediaRepository.Retrieve25RecentPhotosByUserId(Owner.Id);
            RssService service = new RssService();
// ReSharper disable PossibleNullReferenceException
            string fullPath = "http://" + Request.Url.Host + "/";
// ReSharper restore PossibleNullReferenceException
            string feed = service.Render(mediae, Owner, fullPath);

            return Content(feed, "text/xml");
        }

        /// <summary>
        /// Serves Atom a feed.
        /// </summary>
        /// <returns></returns>
        public ActionResult Atom()
        {
            List<Media> mediae = _mediaRepository.Retrieve25RecentPhotosByUserId(Owner.Id);
            AtomService service = new AtomService();
// ReSharper disable PossibleNullReferenceException
            string fullPath = string.Format("http://{0}/", Request.Url.Host);
// ReSharper restore PossibleNullReferenceException
            string feed = service.Render(mediae, Owner, fullPath);

            return Content(feed, "text/xml");
        }
        
        
        /// <summary>
        /// Randoms the album photo.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult RandomAlbumPhoto(string username, int id)
        {
            Media media = _mediaRepository.GetRandomImageByAlbumId(Owner.Id, id);

            string path = HttpContext.Request.PhysicalApplicationPath + "Content\\Images\\blank.gif";
            if(media != null)
            {
                MediaFile mediaFile = media.GetImageByPhotoType(PhotoType.Thumbnail);
                path = string.Format("{0}Images\\{1}\\{2}", HttpContext.Request.PhysicalApplicationPath, Owner.Username, mediaFile.FilePath.Replace("/", "\\"));
            }

            byte[] file = System.IO.File.ReadAllBytes(path);
// ReSharper disable PossibleNullReferenceException
            string extension = string.Format("image/{0}", System.IO.Path.GetExtension(path).Replace(".", ""));
// ReSharper restore PossibleNullReferenceException
            return File(file, extension);
        }

    }
}