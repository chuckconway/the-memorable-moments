using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Paging;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.Domain.Services;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class TagsController : HybridController
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IPagingRepository _pagingRepository;
        private readonly IPagingService _pagingService;
        private readonly ITagRepository _tagRepository;
        private readonly ITagService _tagService;
        private readonly IPersistentCollectionService _persistentCollectionService;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="pagingRepository">The paging repository.</param>
        /// <param name="pagingService">The paging service.</param>
        /// <param name="tagRepository">The tag repository.</param>
        /// <param name="tagService">The tag service.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        public TagsController(IMediaRepository mediaRepository, 
            IPagingRepository pagingRepository,
            IPagingService pagingService, 
            ITagRepository tagRepository,
            ITagService tagService, IPersistentCollectionService persistentCollectionService)
        {
            _mediaRepository = mediaRepository;
            _persistentCollectionService = persistentCollectionService;
            _tagService = tagService;
            _tagRepository = tagRepository;
            _pagingRepository = pagingRepository;
            _pagingService = pagingService;
        }

        /// <summary>
        /// Gets the bread crumbs.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetBreadCrumbs()
        {
            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                     {
                                                         {"home",UrlService.UserRoot()},
                                                         {"tagged", UrlService.UserUrl("tags")}
                                                     };
            return crumbs;
        }

        /// <summary>
        /// Taggeds the specified id.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public ActionResult Index(string tag)
        {
            Tag completeTag = _tagRepository.RetrieveTagByNameAndUserId(tag, Owner.Id);
            string viewName = GetViewName(tag);

            IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            breadCrumbs.Add(viewName, UrlService.UserUrl("tagged/" + HttpUtility.HtmlEncode(tag)));

            TagsIndexView view = ModelFactory<TagsIndexView>(new { Name = viewName, Tag = new Tag(tag) });

            if (completeTag != null)
            {
                List<Tag> relatedTags = _tagRepository.GetRelatedTags(completeTag.Id);
                List<Media> photos = _mediaRepository.RetrieveByTagNameAndUserId(tag, Owner.Id);
                string url = string.Format("{0}/tagged/{1}", Authorization.Owner.Username, tag);
                _persistentCollectionService.SetBackUrl(url, SiteCookie);
                string key = _persistentCollectionService.Set(Authorization.Owner.Username + "_tagged_" + tag, photos, Persistence.Permanent);

                view.Set = key;
                view.Media = photos;
                view.Tag = completeTag;
                view.RelatedTags = _tagService.HyperlinkTheTags(relatedTags, Owner.Username);
            }

            return View(view, breadCrumbs);
        }

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        private static string GetViewName(string tag)
        {
            return (!string.IsNullOrEmpty(tag) ? HttpUtility.HtmlEncode(tag.Replace("-", " ")) : "");
        }

        /// <summary>
        /// Shows the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public ActionResult Show(string id, string page)
        {
            int currentPage = (string.IsNullOrEmpty(page) ? 1 : Convert.ToInt32(page));
            List<AlphabetPage> alphabetPages = _pagingRepository.RetrievePagingByUserId(Owner.Id);
            char selectedLetter = GetSelectedLetter(id, alphabetPages);
            AlphaPaging paging = _pagingService.RetreivePagingByLetterAndUserId(selectedLetter, Owner.Id, currentPage);

            IDictionary<string, string> crumbs = GetBreadCrumbs();
            crumbs.Add("letter", string.Empty);
            crumbs.Add(selectedLetter.ToString(), UrlService.UserUrl(string.Format("tags/show/{0}", selectedLetter)));

            MediaGroupedByTagsView mediaGroupedByTagsView = new MediaGroupedByTagsView
                                                                {
                                                                    MediaByTags = paging.MediaGroupedByTags,
                                                                    Name = selectedLetter.ToString(),
                                                                    AlphabetPagingView = new AlphabetPagingView { AlphabetPages = alphabetPages, SelectedLetter = selectedLetter },
                                                                    NumericPaging = new NumericPaging { CurrentPage = currentPage, PageSize = 5, TotalCount = paging.TagCount },
                                                                    Authorization = Authorization,
                                                                    PersistentCollection = _persistentCollectionService,
                                                                    Cookie = SiteCookie
                                                                };

           // _persistentCollectionService.SetBackUrl(string.Format("{0}/tags/show/{1}", Authorization.Owner.Username, selectedLetter), SiteCookie);
           // _persistentCollectionService.Set(Authorization.Owner.Username + "_tags_letter_" + selectedLetter, new List<Media>(), Persistence.Permanent);

            mediaGroupedByTagsView = SetAuthorizationAndUrlService(mediaGroupedByTagsView);
            return View(mediaGroupedByTagsView, crumbs);
        }


        /// <summary>
        /// Displays this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Display(string id)
        {
            List<Media> media = _mediaRepository.RetrieveByTagNameAndUserId(id, Owner.Id);
            string tagName = GetViewName(id);

            IDictionary<string, string> crumbs = GetBreadCrumbs();
            crumbs.Add(tagName, UrlService.UserUrl("tags/display/" + HttpUtility.HtmlEncode(id)));


            TagsIndexView displayView = ModelFactory<TagsIndexView>(new {Media = media, 
                                              Name = HttpUtility.HtmlEncode(tagName),
                                          });
            
            return View("Index", displayView, crumbs);
        }

        /// <summary>
        /// Gets the selected letter.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="alphabetPages">The alphabet pages.</param>
        /// <returns></returns>
        private static char GetSelectedLetter(string id, IEnumerable<AlphabetPage> alphabetPages)
        {
            return string.IsNullOrEmpty(id)
                // ReSharper disable PossibleNullReferenceException
                       ? (from page in alphabetPages
                          where page.MediaCount > 0
                          select page.Letter).FirstOrDefault().ToCharArray()[0]
                // ReSharper restore PossibleNullReferenceException
                       : id.ToCharArray()[0];
        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Tag retrievedTag = _tagRepository.RetrieveTagByNameAndUserId(id, Owner.Id);
            EditTagModel model = new EditTagModel
                                     {
                                         TagDescription = retrievedTag.Description, 
                                         TagText = retrievedTag.TagText, 
                                         OrginalTagText = retrievedTag.TagText
                                     };
            return View(model);
        }

        /// <summary>
        /// Edits the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditTagModel model)
        {
            Tag tag = _tagRepository.RetrieveTagByNameAndUserId(model.OrginalTagText, Owner.Id);
            Tag newTag = new Tag(model.TagText)
                             {
                                 Description = model.TagDescription ?? string.Empty,
                                 Id = tag.Id
                             };

            _tagRepository.Update(newTag, Owner.Id);
            model.UIMessage = "Tag successfully updated.";

            return View(model);
        }

        /// <summary>
        /// Removes the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(string tag, int id)
        {
            _mediaRepository.RemoveTag(id, tag);
            return Content("1");
        }
    }
}
