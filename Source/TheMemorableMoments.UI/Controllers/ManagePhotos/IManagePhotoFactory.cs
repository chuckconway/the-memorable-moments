using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.ManagePhotos
{
    public interface IManagePhotoFactory
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="viewLink">The view link.</param>
        /// <param name="postView">The post view.</param>
        /// <param name="retrieveMedia">The retrieve media.</param>
        /// <param name="user">The user.</param>
        /// <param name="authorization">The authorization.</param>
        /// <returns></returns>
        ManagePhotosView GetView(string viewLink, ManagePhotoPostView postView, Func<List<Media>> retrieveMedia, Domain.Model.User user, Authorization authorization);
    }
}