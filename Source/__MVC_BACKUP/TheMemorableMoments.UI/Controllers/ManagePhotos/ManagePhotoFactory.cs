using System;
using System.Collections.Generic;
using System.Linq;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Controllers.ManagePhotos.Views;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.ManagePhotos
{
    public class ManagePhotoFactory : IManagePhotoFactory
    {
        readonly ITagService _tagService = DependencyInjection.Resolve<ITagService>();

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="viewLink">The view link.</param>
        /// <param name="postView">The post view.</param>
        /// <param name="retrieveMedia">The retrieve media.</param>
        /// <param name="user">The user.</param>
        /// <param name="authorization">The authorization.</param>
        /// <returns></returns>
        public ManagePhotosView GetView(string viewLink, ManagePhotoPostView postView, Func<List<Media>> retrieveMedia, Domain.Model.User user, Authorization authorization)
        {
            IEnumerable<IRenderManagePhotoView> photoViews = Implementions(user, authorization);
            IRenderManagePhotoView view = new GetManagePhotoView(user, _tagService, authorization);

            foreach (IRenderManagePhotoView photoView in photoViews.Where(photoView => string.Equals(photoView.ButtonName, postView.Submit, StringComparison.InvariantCultureIgnoreCase)))
            {
                view = photoView;
                break;
            }

            return view.RenderView(viewLink, postView, retrieveMedia);
        }

        /// <summary>
        /// Implementions of IRenderManagePhotoView.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="authorization">The authorization.</param>
        /// <returns></returns>
        private  IEnumerable<IRenderManagePhotoView> Implementions(Domain.Model.User user, Authorization authorization )
        {
            return new List<IRenderManagePhotoView>
                       {
                           new AddTagsView(user, _tagService, authorization),
                           new ApplyActionView(user, _tagService, authorization),
                           new GetManagePhotoView(user, _tagService, authorization),
                           new SearchActionView(user, _tagService, authorization)
                       };
        }
    }
}