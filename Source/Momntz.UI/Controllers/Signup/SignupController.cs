using System.Web.Mvc;
using Momntz.UI.Models.Signup;

namespace Momntz.UI.Controllers.Signup
{
    public class SignupController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View(new SignupView());
        }

        /// <summary>
        /// Indexes the specified signup.
        /// </summary>
        /// <param name="signup">The signup.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(SignupView signup)
        {
            return View();
        }
    }
}
