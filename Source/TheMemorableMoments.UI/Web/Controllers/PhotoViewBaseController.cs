using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Controllers.ManagePhotos;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web.Controllers
{
    public abstract class PhotoBaseController : HybridController
    {
        private readonly IManagePhotoFactory _managePhotoService;
        protected readonly IPersistentCollectionService persistentCollectionService;


        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoBaseController"/> class.
        /// </summary>
        /// <param name="managePhotoService">The manage photo service.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        protected PhotoBaseController(IManagePhotoFactory managePhotoService, IPersistentCollectionService persistentCollectionService)
       {
           _managePhotoService = managePhotoService;
           this.persistentCollectionService = persistentCollectionService;
       }

        /// <summary>
        /// Photoes the view.
        /// </summary>
        /// <param name="viewLink">The view link.</param>
        /// <param name="cp">The cp.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult PhotoView(string viewLink, string cp, List<Media> media)
        {
            int page = (string.IsNullOrEmpty(cp) ? 1 : Convert.ToInt32(cp));
            ManagePhotosView view = GetManagePhotoView(viewLink, new ManagePhotoPostView { CP = cp }, () => media);
            view = SetAuthorizationAndUrlService(view);

            view.Set = persistentCollectionService.Set(Authorization.Owner.Username + "Photos" + viewLink, media, Persistence.Permanent);
            persistentCollectionService.SetBackUrl(Authorization.Owner.Username + "/photos/" + viewLink + "/?cp=" + page, SiteCookie);

            return View("Index", view);
        }

        /// <summary>
        /// Photoes the view.
        /// </summary>
        /// <param name="viewLink">The view link.</param>
        /// <param name="postView">The post view.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult PhotoView(string viewLink, ManagePhotoPostView postView, List<Media> media)
        {
            int page = (string.IsNullOrEmpty(postView.CP) ? 1 : Convert.ToInt32(postView.CP));
            string queryValues = (string.IsNullOrEmpty(postView.Text)
                                      ? "?cp=" + page
                                      : "?cp=" + page + "&text=" + HttpUtility.HtmlEncode(postView.Text));
            ManagePhotosView view = GetManagePhotoView(viewLink, postView, () => media);
            view = SetAuthorizationAndUrlService(view);

            view.Set = persistentCollectionService.Set(Authorization.Owner.Username + "Photos" + viewLink + "_" + postView.Text, media, Persistence.Permanent);
            persistentCollectionService.SetBackUrl(Authorization.Owner.Username + "/photos/" + viewLink + "/" + queryValues, SiteCookie);

            return View("Index", view);
        }

                /// <summary>
        /// Gets the manage photo view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="managePhotoPostView">The manage photo post view.</param>
        /// <param name="retreiveMedia">The retreive media.</param>
        /// <returns></returns>
        private ManagePhotosView GetManagePhotoView(string view, ManagePhotoPostView managePhotoPostView, Func<List<Media>> retreiveMedia)
        {
            return _managePhotoService.GetView(view, managePhotoPostView, retreiveMedia, Owner, Authorization);
        }
    }
}