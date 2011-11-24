using System;
using System.Web;
using Chucksoft.Core.Services;

namespace TheMemorableMoments.UI.Web.HttpModules
{
    public class RedirectToPrimaryDomain : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        /// <summary>
        /// Contexts the begin request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        static void ContextBeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            IConfigurationService configurationService = new ConfigurationService();

            string withoutHttp = configurationService.GetValueByKey("rootUrl");
            withoutHttp = withoutHttp.Replace("http://", string.Empty);

            if(!application.Context.Request.Url.Host.Contains(withoutHttp))
            {
                application.Context.Response.Redirect(string.Format("http://{0}{1}", withoutHttp, application.Context.Request.Url.PathAndQuery));
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}