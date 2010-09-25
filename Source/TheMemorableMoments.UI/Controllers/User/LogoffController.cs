using System.Web.Mvc;
using System.Web.Security;
using TheMemorableMoments.UI.Web.Controllers;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class LogOffController : AnonymousController
    {
        /// <summary>
        /// Logoff Current User
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return Redirect("/" + Owner.Username);
        }

    }
}
