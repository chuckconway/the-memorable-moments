using System.Web;
using System.Web.Security;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Controllers
{
    public class AuthenticatedController : HybridController
    {
        /// <summary>
        /// Method called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Contains information about the current request and action.</param>
        protected override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext); 

            HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null)
            {
                User user = SetUserToContext(cookie);

                if (user == null)
                {
                    HttpContext.Response.Redirect("~/Login");
                }
            }
            else
            {
                HttpContext.Response.Redirect("~/Login");
            }
        }

    }
}
