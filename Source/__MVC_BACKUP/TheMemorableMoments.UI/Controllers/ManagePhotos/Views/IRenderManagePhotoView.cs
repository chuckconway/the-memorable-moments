using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views.Photos;

namespace TheMemorableMoments.UI.Controllers.ManagePhotos.Views
{
    public interface IRenderManagePhotoView
    {
        /// <summary>
        /// Gets the name of the button.
        /// </summary>
        /// <value>The name of the button.</value>
        string ButtonName { get; }

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <returns></returns>
        ManagePhotosView RenderView(string viewLink, ManagePhotoPostView postView, Func<List<Media>> retrieve);
    }
}