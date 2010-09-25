using System.Collections.Generic;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class YearController : HybridController
    {
        private readonly IRecentRepository _recentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagService _tagService;
        private readonly IPersistentCollectionService _persistentCollectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="YearController"/> class.
        /// </summary>
        /// <param name="recentRepository">The recent repository.</param>
        /// <param name="tagService">The tag service.</param>
        /// <param name="tagRepository">The tag repository.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        public YearController(IRecentRepository recentRepository, ITagService tagService, ITagRepository tagRepository, IPersistentCollectionService persistentCollectionService)
        {
            _recentRepository = recentRepository;
            _persistentCollectionService = persistentCollectionService;
            _tagRepository = tagRepository;
            _tagService = tagService;
        }

        /// <summary>
        /// Taggeds the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            List<Tag> tags = _tagRepository.GetRelatedTagsByYear(Owner.Id, id);
            List<Media> photos = _recentRepository.RetrieveYearByUserId(id, Owner.Id);
            string url = string.Format("{0}/year/{1}", Authorization.Owner.Username, id);
            _persistentCollectionService.SetBackUrl(url, SiteCookie);
            string key = _persistentCollectionService.Set(Authorization.Owner.Username + "_year_" + id, photos, Persistence.Permanent);

            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                     {
                                                         {"home", UrlService.UserRoot()},
                                                         {"year", string.Empty},
                                                         {id.ToString(),  UrlService.UserUrl("year/" + id)}
                                                     };
            
            YearIndexView view = ModelFactory<YearIndexView>(new {Year = id, Media = photos, RelatedTags = _tagService.HyperlinkTheTags(tags, Owner.Username), Set = key});
            return View(view, crumbs);
        }

    }
}
