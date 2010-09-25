using System.Collections.Generic;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model.Recent;
using TheMemorableMoments.UI.Models.Views.Recent;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class RecentController : HybridController
    {
        private readonly IRecentRepository _recentRepository;
        private readonly IPersistentCollectionService _persistentCollectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentController"/> class.
        /// </summary>
        /// <param name="recentRepository">The recent repository.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        public RecentController(IRecentRepository recentRepository, IPersistentCollectionService persistentCollectionService)
        {
            _recentRepository = recentRepository;
            _persistentCollectionService = persistentCollectionService;
        }

        /// <summary>
        /// Indexes the specified photo count.
        /// </summary>
        /// <param name="photoCount">The photo count.</param>
        /// <returns></returns>
        public ActionResult Index(int photoCount = 100)
        {
            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                     {
                                                         {"home", UrlService.UserRoot()},
                                                         {"recent uploads", UrlService.UserUrl("recent")}
                                                     };
            List<RecentUploads> recentUploads = _recentRepository.RetrieveRecentUploads(Owner.Id, photoCount);
            RecentIndex recentIndex = ModelFactory<RecentIndex>(new 
                                          {
                                              RecentUploads = recentUploads,
                                              PersistentCollection = _persistentCollectionService,
                                              Cookie = SiteCookie
                                          });

            return View(recentIndex, crumbs);
        }
    }
}
