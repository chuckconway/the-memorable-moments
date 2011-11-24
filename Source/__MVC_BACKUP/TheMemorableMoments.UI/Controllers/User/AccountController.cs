using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Chucksoft.Core.Services;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Validation;

namespace TheMemorableMoments.UI.Controllers.User
{
    [Authorize]
    public class AccountController : DashboardBaseController
    {
        private readonly ICryptographyService _cryptographyService;
        private readonly IUserRepository _userRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="cryptographyService">The cryptography service.</param>
        /// <param name="userRepository">The user repository.</param>
        public AccountController(ICryptographyService cryptographyService, IUserRepository userRepository)
        {
            _cryptographyService = cryptographyService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Manages the details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            IDictionary<string, string> crumbs = GetBreadCrumbs("details", UrlService.UserUrl("account"));
            string password = _cryptographyService.Decrypt(Owner.Password);
            AccountDetailsView view = ModelFactory<AccountDetailsView>(new {password, Confirm = password});
            view.Populate(Owner);

            return View(view, crumbs);
        }

        /// <summary>
        /// Manages the details.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="detailsView">The details view.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string username, AccountDetailsView detailsView)
        {
            IDictionary<string, string> crumbs = GetBreadCrumbs("details", UrlService.UserUrl("account"));

            if (ModelState.IsValid)
            {
                detailsView.Password = _cryptographyService.Encrypt(detailsView.Password);
                Domain.Model.User savedUser = detailsView.GetUser();
                savedUser.Settings = Owner.Settings;
                savedUser.Id = Owner.Id;
                _userRepository.Save(savedUser);

                detailsView.UIMessage = "Account details saved.";
                detailsView.Password = _cryptographyService.Decrypt(savedUser.Password);
                detailsView = SetAuthorizationAndUrlService(detailsView);
                detailsView.Authorization.Owner = savedUser;
            }
            else
            {
                ValidationHelper.ValidationHackRemoveNameAndBlankKey(ModelState);
            }

            return View(detailsView, crumbs);
        }

        /// <summary>
        /// Save User Settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Settings()
        {
            IDictionary<string, string> crumbs = GetBreadCrumbs("settings", UrlService.UserUrl("settings"));
            SettingsView view = Mapper.Map<UserSettings, SettingsView>(Owner.Settings);
            view = SetAuthorizationAndUrlService(view);

            return View(view, crumbs);
        }


        /// <summary>
        /// Gets the bread crumbs.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetBreadCrumbs(string text, string url)
        {
            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                     {
                                                         {"home", UrlService.UserRoot()},
                                                         {"account", UrlService.UserUrl("account")},
                                                         {text, url}
                                                     };
            return crumbs;
        }

        /// <summary>
        /// Save User Settings
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Settings(SettingsView form)
        {
            IDictionary<string, string> crumbs = GetBreadCrumbs("settings", UrlService.UserUrl("settings"));
            SetAuthorizationAndUrlService(form);

            if (ModelState.IsValid)
            {
                Domain.Model.User user = form.GetSettings(Owner);
                form.UIMessage = "Account settings saved.";

                _userRepository.Save(user);
            }

            return View(form, crumbs);
        }

    }
}