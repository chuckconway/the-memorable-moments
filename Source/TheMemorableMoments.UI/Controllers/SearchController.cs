using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers
{
    public class SearchController : AnonymousController
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IPaginationService<Media> _paginationService;
        private readonly IPersistentCollectionService _persistentCollectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="paginationService">The pagination service.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        public SearchController(IMediaRepository mediaRepository, IPaginationService<Media> paginationService, IPersistentCollectionService persistentCollectionService)
        {
            _mediaRepository = mediaRepository;
            _persistentCollectionService = persistentCollectionService;
            _paginationService = paginationService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string text, string cp)
        {
            List<Media> media = (!string.IsNullOrEmpty(text) ? _mediaRepository.Search(text) : new List<Media>());
            string encodedText = HttpUtility.HtmlEncode(text);
            int page = (string.IsNullOrEmpty(cp) ? 1 : Convert.ToInt32(cp));

            string setKey = _persistentCollectionService.Set("global_search" + encodedText, media, Persistence.Temporary);
            _persistentCollectionService.SetBackUrl(string.Format("search?cp={0}&text={1}", page, encodedText), SiteCookie);

            SearchView view = new SearchView { SearchText = encodedText, TotalResults = media.Count, Pagination = _paginationService, Set = setKey };
            view.Pagination.GeneratePaging(media, page, 20, "?cp={0}&text=" + encodedText);
            return View(view);
        }
        
    }
}
