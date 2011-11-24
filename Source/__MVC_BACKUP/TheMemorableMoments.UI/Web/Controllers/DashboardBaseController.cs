using System.Web.Mvc;

namespace TheMemorableMoments.UI.Web.Controllers
{
    public class DashboardBaseController : AuthenticatedController
    {
        /// <summary>
        /// Method called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Contains information about the current request and action.</param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (SignedInUser != null && SignedInUser.Id != Owner.Id)
            {
                Response.Redirect("/" + Owner.Username);
            }
        }
    }
}
