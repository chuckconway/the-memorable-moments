using System.Web.Mvc;
using System.Web.Routing;
using TheMemorableMoments.Infrastructure.Repositories;
using TheMemorableMoments.UI.Models.Views;

namespace TheMemorableMoments.UI.Controllers
{
    public class JoinController : Controller
    {
        private readonly IWaitingListRepository _waitingListRepository;
        private readonly IJoinRepository _joinRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinController"/> class.
        /// </summary>
        /// <param name="waitingListRepository">The waiting list repository.</param>
        /// <param name="joinRepository">The join repository.</param>
        public JoinController(IWaitingListRepository waitingListRepository, IJoinRepository joinRepository)
        {
            _waitingListRepository = waitingListRepository;
            _joinRepository = joinRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View(new JoinView());
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string token, string email)
        {
            JoinView joinView = new JoinView { UIMessage = "Oops, we couldn&#39;t process your request, your information must have been lost in transit. Could you try again?" };
            ActionResult actionResult = View(joinView);

            if(!string.IsNullOrEmpty(email))
            {
                actionResult = GetEmailResult(joinView, email);
            }
            else if(!string.IsNullOrEmpty(token))
            {
                actionResult = GetTokenResult(joinView, token);
            }

            return actionResult;
        }

        /// <summary>
        /// Gets the email result.
        /// </summary>
        /// <param name="joinView">The join view.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        private ActionResult GetEmailResult(JoinView joinView, string email)
        {
            _waitingListRepository.Insert(email);
            joinView.UIMessage = "Thank you for your interest. You have been added to our waiting list.";
            ActionResult actionResult = View(joinView);
            return actionResult;
        }

        /// <summary>
        /// Gets the token result.
        /// </summary>
        /// <param name="joinView">The join view.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private ActionResult GetTokenResult(JoinView joinView, string token)
        {
            ActionResult actionResult;
            int tokenCount = _joinRepository.GetTokenCount(token);
            bool validToken = (tokenCount == 1);

            if(validToken)
            {
                actionResult = RedirectToAction(string.Empty, "register", new RouteValueDictionary{{"token", token}});
            }
            else
            {
                joinView.UIMessage = "We searched our records forwards and backwards and we were unable to find the token entered. Perhaps it was mistyped?";
                actionResult = View(joinView);
            }
            return actionResult;
        }
    }
}
