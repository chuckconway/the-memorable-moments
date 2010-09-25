using System;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web.ModelBinding
{
    public class AlbumModelBinder : BaseModelBinder, IModelBinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumModelBinder"/> class.
        /// </summary>
        public AlbumModelBinder() : base(DependencyInjection.Resolve<IUserAuthorization>()) { }

        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns>The bound value.</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var albumRepository = DependencyInjection.Resolve<IAlbumRepository>();
            User user = GetUser(controllerContext);

            int albumId = Convert.ToInt32(controllerContext.RouteData.Values["id"]);
            Album album = albumRepository.RetrieveByUserIdAndPrimayKey(user.Id, albumId);

            return album;
        }
    }
}