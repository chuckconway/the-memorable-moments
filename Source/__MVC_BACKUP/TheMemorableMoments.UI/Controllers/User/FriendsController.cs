using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views.Friends;
using TheMemorableMoments.UI.Web.Controllers;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class FriendsController : DashboardBaseController
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IInvitationRepository _invitationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendsController"/> class.
        /// </summary>
        /// <param name="friendRepository">The friend repository.</param>
        /// <param name="invitationRepository">The invitation repository.</param>
        public FriendsController(IFriendRepository friendRepository, IInvitationRepository invitationRepository)
        {
            _friendRepository = friendRepository;
            _invitationRepository = invitationRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            int count = GetCount();

            IDictionary<string, string> crumbs = GetBreadCrumbs();
            crumbs.Add("find", UrlService.UserUrl("friends"));
            FriendsView friendsView = ModelFactory<FriendsView>(new {FriendsCount = count, Friends = new List<Friend>()});

            return View(friendsView, crumbs);
        }
        
        /// <summary>
        /// Gets the bread crumbs.
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetBreadCrumbs()
        {
            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                     {
                                                         {"home", UrlService.UserRoot()},
                                                         {"friends", UrlService.UserUrl("friends/all")}
                                                     };
            return crumbs;
        }
        
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <returns></returns>
        private int GetCount()
        {
            List<Friend> friends = _friendRepository.RetrieveFriendsByUserId(Owner.Id);
            return friends.Count;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string searchText)
        {  
            int count = GetCount();

            List<Friend> friends = _friendRepository.FindFriends(searchText, Owner.Id);
            IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            FriendsView friendsView = ModelFactory<FriendsView>(new { FriendsCount = count, Friends = friends, Query = searchText });

            return View(friendsView, breadCrumbs);
        }

        /// <summary>
        /// Renders the All View
        /// </summary>
        /// <returns></returns>
        public ActionResult All()
        {
            List<Friend> friends = _friendRepository.RetrieveFriendsByUserId(Owner.Id);

            IDictionary<string, string> crumbs = GetBreadCrumbs();
            crumbs.Add("all", UrlService.UserUrl("all"));
            FriendAllView view = ModelFactory<FriendAllView>(new { friends });

            return View(view, crumbs);
        }

        /// <summary>
        /// Adds the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Add(string id)
        {
            int friendId = Convert.ToInt32(id);
            _friendRepository.Insert(friendId, Owner.Id);

            return Content("Turn Javascript on");
        }

        /// <summary>
        /// Adds the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Remove(string id)
        {
            int friendId = Convert.ToInt32(id);
            _friendRepository.Remove(friendId, Owner.Id);

            return Content("Turn Javascript on");
        }

        /// <summary>
        /// Renders the Invite view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Invite()
        {
            FriendInviteView friendInviteView = GetFriendInviteView();
            IDictionary<string, string> crumbs = GetCrumbs();

            return View(friendInviteView, crumbs);
        }

        /// <summary>
        /// Gets the friend invite view.
        /// </summary>
        /// <returns></returns>
        private FriendInviteView GetFriendInviteView()
        {
            int count = GetCount();
            byte invitationsLeft = _invitationRepository.InvitationsLeft(Owner.Id);
            return ModelFactory<FriendInviteView>(new{ RemainingInvitationsCount = invitationsLeft, FriendsCount = count});
        }

        /// <summary>
        /// Renders the Invite view.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Invite(string[] email)
        {
            FriendInviteView friendInviteView = GetFriendInviteView();
            IDictionary<string, string> crumbs = GetCrumbs();

            if (email != null)
            {
                string[] emails = email.Where(o => !string.IsNullOrEmpty(o)).ToArray();

                _invitationRepository.Add(new Invitation { Email = emails, UserId = Owner.Id });
                byte invitationsLeft = _invitationRepository.InvitationsLeft(Owner.Id);
                string message = string.Format("{0} invitation(s) send. {1} invitations remaining.", emails.Length, invitationsLeft);

                friendInviteView.RemainingInvitationsCount = invitationsLeft;
                friendInviteView.UIMessage = message;
            }

            return View(friendInviteView, crumbs);
        }

        /// <summary>
        /// Gets the crumbs.
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetCrumbs()
        {
            IDictionary<string, string> crumbs = GetBreadCrumbs();
            crumbs.Add("invite", UrlService.UserUrl("invite"));
            return crumbs;
        }
    }
}