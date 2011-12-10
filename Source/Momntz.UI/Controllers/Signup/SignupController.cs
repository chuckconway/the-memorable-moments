using System.Web.Mvc;
using Momntz.Exceptions;
using Momntz.UI.Models.Signup;
using Momntz.UI.Web.Controllers;

namespace Momntz.UI.Controllers.Signup
{
    public class SignupController : UnAuthenticatedController
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
            try
            {
                var success = RedirectToAction("Welcome");
                return Form(signup, success);
            }
            catch (DuplicateUsernameException)
            {
                //TODO:Add duplicate error message too error validation framework when it get put into place.
                return View(signup);
            }
        }

        /// <summary>
        /// Welcomes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Welcome()
        {
            return View();
        }
    }
}
