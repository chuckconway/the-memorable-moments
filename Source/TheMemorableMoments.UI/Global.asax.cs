using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using Chucksoft.Core.Web.CacheProviders;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Web;
using TheMemorableMoments.UI.Web.AutoMapper;
using TheMemorableMoments.UI.Web.ModelBinding;

namespace TheMemorableMoments.UI
{
    public class MvcApplication : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;

        /// <summary>
        /// Gets the container provider.
        /// </summary>
        /// <value>The container provider.</value>
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("content/{*pathInfo}");

            IUserRepository userRepository = DependencyInjection.Resolve<IUserRepository>();
            ICacheProvider cacheProvider = DependencyInjection.Resolve<ICacheProvider>();
            routes.MapRoute("RegisterAction", "Register/{action}/{id}", new { controller = "register", action = "Index", id = "" });


            //Username controllers
            routes.MapRoute("Services", "{username}/Services/{action}/{id}", new { controller = "Services", id = "" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("Year", "{username}/Year/{id}", new { controller = "Year", action = "Index", id = "" }, new { id = @"\d*", username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("Recent", "{username}/Recent/{action}/{id}", new { controller = "Recent", action = "Index", id = "" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("Tagged", "{username}/Tags/{action}/{id}", new { controller = "Tags", action = "Show", id = "" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("TaggedWithoutAction", "{username}/Tagged/{tag}", new { controller = "Tags", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserAccount", "{username}/Account/{action}", new { controller = "Account", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserSettings", "{username}/Settings/{action}", new { controller = "Settings", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserComments", "{username}/Comments/{action}", new { controller = "Comments", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserCommentsWithId", "{username}/Comments/{action}/{id}", new { controller = "Comments", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserFriends", "{username}/Friends/{action}", new { controller = "Friends", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserFriendsWithId", "{username}/Friends/{action}/{id}", new { controller = "Friends", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserAlbums", "{username}/Albums/{action}", new { controller = "Albums", action = "Index" }, new { action = "([A-Z]|[a-z])*", username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserAlbumsWithId", "{username}/Albums/{action}/{id}", new { controller = "Albums", id = "" }, new { action = "([A-Z]|[a-z])*", username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserAlbumsAndId", "{username}/Albums/{id}", new { controller = "Albums", action = "Index", id = "" }, new { id = @"\d*", username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserAlbumsAndIdAddPhoto", "{username}/Albums/{id}/addphotos/{view}", new { controller = "Albums", action = "addphotos", id = "", view = "" }, new { id = @"\d*", username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserUpload", "{username}/Upload/{action}/{id}", new { controller = "Upload", action = "Index", id = "" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserLogoff", "{username}/Logoff/{action}", new { controller = "Logoff", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserPhotosEdit", "{username}/Photos/Edit/{set}", new { controller = "Photos", action = "Edit" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserPhotosShow", "{username}/Photos/Show/{set}", new { controller = "Photos", action = "Show" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserPhotos", "{username}/Photos/{action}", new { controller = "Photos", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserPhotosWithId", "{username}/Photos/{action}/{id}", new { controller = "Photos", action = "Index", id = "" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserDashboard", "{username}/Dashboard/{action}", new { controller = "Dashboard", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("User", "{username}/{action}", new { controller = "User", action = "Index" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            routes.MapRoute("UserWithId", "{username}/{action}/{id}", new { controller = "User", action = "Index", id = "" }, new { username = new UserRouteConstraint(userRepository, cacheProvider) });
            
            
            routes.MapRoute("MediaUpload", "MediaUpload/Photos/{id}/{batch}", new { controller = "MediaUpload", action = "Photos", id = "" });
            routes.MapRoute("Actions", "{controller}/{action}/{id}", new { action = "Index", id = "" });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new {controller="home", action = "Index", id = "" });
        }

        /// <summary>
        /// Application_s the start.
        /// </summary>
// ReSharper disable InconsistentNaming
        protected void Application_Start()
// ReSharper restore InconsistentNaming
        {
            IContainer container = DependencyInjection.Container;
            _containerProvider = new ContainerProvider(container);

            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(ContainerProvider));
            AreaRegistration.RegisterAllAreas();
            AutoMapperMappings.Initialize();

            ModelBinders.Binders.Add(typeof(Media), new MediaModelBinder());
            ModelBinders.Binders.Add(typeof(Album), new AlbumModelBinder());
            RegisterRoutes(RouteTable.Routes);
            
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
        
    }
}