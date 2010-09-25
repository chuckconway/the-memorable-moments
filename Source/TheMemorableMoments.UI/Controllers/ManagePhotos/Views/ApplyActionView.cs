using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.ManagePhotos.Views
{
    public class ApplyActionView : ManagePhotoBase, IRenderManagePhotoView
    {
        private readonly ITagService _tagService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplyActionView"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="tagService">The tag service.</param>
        /// <param name="authorization">The authorization.</param>
        public ApplyActionView(Domain.Model.User user, ITagService tagService, Authorization authorization)
            : base(user, authorization)
        {
            _tagService = tagService;
        }

        /// <summary>
        /// Gets the name of the button.
        /// </summary>
        /// <value>The name of the button.</value>
        public string ButtonName
        {
            get { return "apply"; }
        }

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="postView">The post view.</param>
        /// <param name="retrieve">The retrieve.</param>
        /// <returns></returns>
        public ManagePhotosView RenderView(string view, ManagePhotoPostView postView, Func<List<Media>> retrieve)
        {
            UpdatePhotosWithApplyAction(postView);

            ManagePhotosView managePhotosView = RenderMedia(postView, retrieve, "ManagedPhotoView");
            managePhotosView.RenderAdminTags = _tagService.HyperlinkTheAdminTags;

            managePhotosView.BreadCrumbs = SetBreadCrumb(view);
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            string viewName = cultureInfo.TextInfo.ToTitleCase(postView.MediaStatus);
            string friendlyViewName = (string.Equals(viewName, "Innetwork", StringComparison.InvariantCultureIgnoreCase)
                                           ? "In Network"
                                           : viewName);
            if (postView.MediaId != null && postView.MediaId.Length > 0)
            {
                managePhotosView.UIMessage = string.Format("{0} photo(s) moved to {1}.",
                                                           postView.MediaId.Length, friendlyViewName);
            }

            managePhotosView.TabName = view;
            return managePhotosView;
        }
                    /// <summary>
        /// Updates the photos with apply action.
        /// </summary>
        /// <param name="postView">The post view.</param>
        private void UpdatePhotosWithApplyAction(ManagePhotoPostView postView)
        {
            MediaStatus mediaStatus = postView.MediaStatus.ParseEnum<MediaStatus>();

            if (postView.MediaId != null && postView.MediaId.Length > 0)
            {
                mediaRepository.UpdateStatus(postView.MediaId, user.Id, mediaStatus);
            }
        }
    }
}