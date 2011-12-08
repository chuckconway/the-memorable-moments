using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Momntz.UI.Web.Injection;
using Raven.Client.Document;

namespace Momntz.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("signup", "signup/{action}", new { controller = "signup", action = "Index", });
            routes.MapRoute("home", string.Empty, new { controller = "Home", action = "Index",  });

        }

        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            InitializeContainer();
            IntegrateWithNHProf();
        }

        private void IntegrateWithNHProf()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        protected void Application_EndRequest()
        {
            // Make sure to dispose of NHibernate session if created on this web request
            IoC.ReleaseAndDisposeAllHttpScopedObjects();
        }

        /// <summary>
        /// Initializes the container.
        /// </summary>
        public void InitializeContainer()
        {
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}