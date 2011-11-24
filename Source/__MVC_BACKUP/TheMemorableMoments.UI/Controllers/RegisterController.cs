using System.Web.Mvc;
using Chucksoft.Storage;
using Chucksoft.Web.Mvc.Common.Authentication;

using TheMemorableMoments.UI.Models.Views.UserModels;
using TheMemorableMoments.UI.Web.Validation;

namespace TheMemorableMoments.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IStorage _fileService;
        private readonly IUserSession<Domain.Model.User> _userSession;
        private readonly IJoinRepository _joinRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="fileService">The file service.</param>
        /// <param name="userSession">The user session.</param>
        /// <param name="joinRepository">The join repository.</param>
        public RegisterController(IUserRepository userRepository, IStorage fileService, IUserSession<Domain.Model.User> userSession, IJoinRepository joinRepository)
        {
            _userRepository = userRepository;
            _joinRepository = joinRepository;
            _fileService = fileService;
            _userSession = userSession;
        }


        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string token)
        {
            ActionResult actionResult = RedirectToAction(string.Empty, "join");

            //another fucking hack....
            //If not removed, it triggers the validation on the get.
            ModelState.Remove("token");

            if (!string.IsNullOrEmpty(token))
            {
                int tokenCount = _joinRepository.GetTokenCount(token);
                bool validToken = (tokenCount == 1);

                if (validToken)
                {
                    actionResult = View(new UserRegisterView());
                }
            }

            return actionResult;
        }

        /// <summary>
        /// Checks the uniqueness.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckAvailability(string id)
        {
            //1  is the username is unavailable
            int count = 1;

            if (!string.IsNullOrEmpty(id) && id.Length > 2)
            {
                count = _userRepository.CheckAvailability(id);
            }

            return Content(count.ToString());
        }

        /// <summary>
        /// Registers the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(UserRegisterView view, string token)
        {
            ActionResult actionResult = RedirectToAction(string.Empty, "join");

            if (!string.IsNullOrEmpty(token))
            {
                int tokenCount = _joinRepository.GetTokenCount(token);
                bool validToken = (tokenCount == 1);

                if (validToken)
                {
                    actionResult = GetResult(view);
                }
            }

            return actionResult;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        protected ActionResult GetResult(UserRegisterView view)
        {
// ReSharper disable Asp.NotResolved
            ActionResult action = View(view);
// ReSharper restore Asp.NotResolved

            if (ModelState.IsValid)
            {
                action = SaveNewUser(view);
            }
            else
            {
                ValidationHelper.ValidationHackRemoveNameAndBlankKey(ModelState);
                view.ErrorMessage = "<span class='errormessage' >Oops, one or more errors have occured. Please verify your entries and try again.</span>";
            }

            return action;
        }
        

        /// <summary>
        /// Saves the new user.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        private ActionResult SaveNewUser(UserRegisterView view)
        {
            Domain.Model.User user = view.GetUser();
            _fileService.CreateBucket(user.Username.ToLower());
            _userRepository.Save(user);

            Domain.Model.User newUser = _userRepository.RetrieveUserByLoginCredentials(user.Username, user.Password);
            _userSession.Login(newUser, false, string.Format("~/{0}/dashboard", newUser.Username));
            ActionResult action = RedirectToAction("Index", user.Username);
            return action;
        }
    }
}
