using System;
using System.Linq;
using System.Web.Mvc;
using Chucksoft.Core.Web.Mvc.Validation;
using Chucksoft.Web.Mvc.Common.Authentication;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Authentication;

namespace TheMemorableMoments.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuthentication _userAuthentication;
        private readonly IUserSession<Domain.Model.User> _userSession;


        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController(IUserAuthentication userAuthentication, IUserSession<Domain.Model.User> userSession)
        {
            _userAuthentication = userAuthentication;
            _userSession = userSession;
        }

        /// <summary>
        /// Indexes this instance. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginForm());
        }

        /// <summary>
        /// The index instance.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(LoginForm form)
        {
            ActionResult view = View(new LoginForm());
            Validator<LoginForm> validator = new Validator<LoginForm>(form);

            form.ErrorMessage = "Username or password is invalid.";
            if (validator.IsValid())
            {
                form.ErrorMessage = ValidateUser(form, (Request.UrlReferrer != null ? Request.UrlReferrer.AbsolutePath : string.Empty));
                view = View("index", form);
            }

            return view;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="referer">The referer.</param>
        /// <returns></returns>
        private string ValidateUser(LoginForm form, string referer)
        {
            string errorMessage;

            if (!string.IsNullOrEmpty(form.Username) && !string.IsNullOrEmpty(form.Password))
            {
                errorMessage = RedirectSuccessfulLoginOrRetrievesErrorMessage(form, referer);
            }
            else
            {
                errorMessage = "Both a username and a password are required to login.";
            }

            return errorMessage;
        }

        /// <summary>
        /// Logs the User out.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            _userSession.LogUserOut();
            return View(new LoginForm());
        }
        
        /// <summary>
        /// Redirects the successful login or retrieves error message.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="referer">The referer.</param>
        /// <returns></returns>
        private string RedirectSuccessfulLoginOrRetrievesErrorMessage(LoginForm form, string referer)
        {
            string errorMessage = string.Empty;

            if (_userAuthentication.IsValid(form.Username, form.EncryptedPassword))
            {
                string path = GetUrlForAuthenticatedUser(form.Username, referer);
                _userSession.Login(_userAuthentication.User, form.RememberMe, path);
            }
            else
            {
                errorMessage = "Invalid username or password.";
            }

            return errorMessage;
        }

        /// <summary>
        /// Gets the URL for authenticated user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="referer">The referer.</param>
        /// <returns></returns>
        private static string GetUrlForAuthenticatedUser(string username, string referer)
        {
            string[] illegalPaths = { "LOGIN", "JOIN", "REGISTER", "ABOUT", "DIRECTORY" };

            Func<string, string> evaluteAndSetPath = o => (illegalPaths.Any(o.ToUpper().Contains) ||
                                                           string.IsNullOrEmpty(o) || o.Length == 1
                                                               ? string.Format("~/{0}", username)
                                                               : o);

            string returnPath = evaluteAndSetPath(referer);
            return returnPath;

        }
    }
}
