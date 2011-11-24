using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.ManagePhotos.Views
{
    public class SearchActionView : ManagePhotoBase, IRenderManagePhotoView
    {
        private readonly ITagService _tagService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetManagePhotoView"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="tagService">The tag service.</param>
        /// <param name="authorization">The authorization.</param>
        public SearchActionView(Domain.Model.User user, ITagService tagService, Authorization authorization)
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
            get { return "search"; }
        }

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <returns></returns>
        public ManagePhotosView RenderView(string view, ManagePhotoPostView postView, Func<List<Media>> retrieve)
        {
            ManagePhotosView managePhotosView = RenderMedia(postView, retrieve, "SearchView", "?cp={0}&text=" + postView.Text);
            managePhotosView.Text = postView.Text;
            managePhotosView.BreadCrumbs = SetBreadCrumb(view);
            managePhotosView.TabName = view;
            managePhotosView.RenderAdminTags = _tagService.HyperlinkTheAdminTags;

            return managePhotosView;
        }
    }
}