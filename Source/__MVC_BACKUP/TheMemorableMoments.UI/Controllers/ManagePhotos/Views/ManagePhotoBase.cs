using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.ManagePhotos.Views
{
    public abstract class ManagePhotoBase
    { 
        protected readonly IUserUrlService urlService;
        protected readonly Domain.Model.User user;
        protected readonly IMediaRepository mediaRepository;
        protected ITagRepository tagRepsitory;
        protected Authorization authorization;
        private readonly IPaginationService<Media> _paginationService;


        /// <summary>
        /// Initializes a new instance of the <see cref="ManagePhotoBase"/> class.
        /// </summary>
        /// <param name="urlService">The URL service.</param>
        /// <param name="user">The user.</param>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="tagRepsitory">The tag repsitory.</param>
        /// <param name="paginationService">The pagination service.</param>
        /// <param name="authorization">The authorization.</param>
        protected ManagePhotoBase(
            IUserUrlService urlService, 
            Domain.Model.User user, 
            IMediaRepository mediaRepository,
            ITagRepository tagRepsitory, IPaginationService<Media> paginationService, Authorization authorization)
        {
            this.urlService = urlService;
            _paginationService = paginationService;
            this.tagRepsitory = tagRepsitory;
            this.mediaRepository = mediaRepository;
            this.user = user;
            this.authorization = authorization;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagePhotoBase"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="authorization">The authorization.</param>
        protected ManagePhotoBase(Domain.Model.User user, Authorization authorization)
            : this(UserUrlService.GetInstance(authorization.Owner),
            user, DependencyInjection.Resolve<IMediaRepository>(),
            DependencyInjection.Resolve<ITagRepository>(), DependencyInjection.Resolve<IPaginationService<Media>>(), authorization)
        {

        }


        /// <summary>
        /// Sets the bread crumb.
        /// </summary>
        /// <param name="currentView">The current view.</param>
        protected List<BreadCrumb> SetBreadCrumb(string currentView)
        {
            return SetBreadCrumb(currentView, currentView);
        }

        /// <summary>
        /// Sets the bread crumb.
        /// </summary>
        /// <param name="currentView">The current view.</param>
        /// <param name="text">The text.</param>
        private List<BreadCrumb> SetBreadCrumb(string currentView, string text)
        {
            List<BreadCrumb> crumbs = new List<BreadCrumb>
                                          {
                                              new BreadCrumb{ Href = urlService.UserRoot(), Text = "home"},
                                              new BreadCrumb{ Text = "photos"},
                                              new BreadCrumb{ Href = urlService.UserUrl("photos/" + currentView), Text = text}
                                          };
            return crumbs;
        }

        /// <summary>
        /// Renders the media.
        /// </summary>
        /// <param name="postView">The post view.</param>
        /// <param name="retrieve">The retrieve.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <returns></returns>
        protected ManagePhotosView RenderMedia(ManagePhotoPostView postView, Func<List<Media>> retrieve, string viewName)
        {
            return RenderMedia(postView, retrieve, viewName, "?cp={0}");
        }

        /// <summary>
        /// Renders the apply action.
        /// </summary>
        /// <param name="postView">The post view.</param>
        /// <param name="retrieve">The retrieve.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        protected ManagePhotosView RenderMedia(ManagePhotoPostView postView, Func<List<Media>> retrieve, string viewName, string format)
        {
            ManagePhotosView view = new ManagePhotosView();
            List<Media> media = retrieve();

            int index = (string.IsNullOrEmpty(postView.CP) ? 1 : Convert.ToInt32(postView.CP));

            view.MediaStatusCount = mediaRepository.GetMediaStatusCountByUserId(user.Id);
            view.Pagination = _paginationService;
            view.TotalResults = media.Count;
            view.PartialView = viewName;
            view.UIMessage = string.Empty;
            view.Authorization = authorization;
            view.Pagination.GeneratePaging(media, index, 20, format);

            return view;
        }

    }
}