using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web.Controllers
{
    public class AnonymousController : BaseController
    {
        private readonly IUserAuthorization _userAuthorization;
        
        /// <summary>
        /// Gets the user security information for the current HTTP request.
        /// </summary>
        /// <value></value>
        /// <returns>The user security information for the current HTTP request.</returns>
        public User Owner { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardBaseController"/> class.
        /// </summary>
        /// <param name="userAuthorization">The user authorization.</param>
        protected AnonymousController(IUserAuthorization userAuthorization)
        {
            _userAuthorization = userAuthorization;
        }

        /// <summary>
        /// Gets or sets the URL service.
        /// </summary>
        /// <value>The URL service.</value>
        public IUserUrlService UrlService { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousController"/> class.
        /// </summary>
        public AnonymousController() : this(DependencyInjection.Resolve<IUserAuthorization>()){}

        /// <summary>
        /// Create a new instance and sets the Authorization, UrlSerivce and Maps the values passed in to the new instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T ModelFactory<T>(object values) where T :IBaseModel, new()
        {
            T instance = Mapper.DynamicMap<T>(values);
            instance.Authorization = GetAuthorization();
            instance.UrlService = UrlService;

            return instance;
        }

        /// <summary>
        /// Gets the authorization.
        /// </summary>
        /// <returns></returns>
        protected virtual Authorization GetAuthorization()
        {
            return new Authorization(false, false, Owner, null);
        }

        /// <summary>
        /// Create a new instance and sets the Authorization and UrlSerivce
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T ModelFactory<T>() where T : IBaseModel, new()
        {
            T instance = new T
            {
                Authorization = GetAuthorization(),
                UrlService = UrlService
            };

            return instance;
        }

        /// <summary>
        /// Sets the Authorization and UrlService on the Model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T SetAuthorizationAndUrlService<T>(T instance) where T : IBaseModel
        {
            instance.Authorization = GetAuthorization();
            instance.UrlService = UrlService;

            return instance;
        }


        /// <summary>
        /// Views the specified view name.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <param name="breadCrumbs">The bread crumbs.</param>
        /// <returns></returns>
        public ActionResult View(string viewName, object model, IDictionary<string, string> breadCrumbs)
        {
            AddUserDataToView(model, breadCrumbs);
            return View(viewName, model);
        }


        /// <summary>
        /// Views the specified view name.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="breadCrumbs">The bread crumbs.</param>
        /// <returns></returns>
        public ActionResult View(object model, IDictionary<string, string> breadCrumbs)
        {
            AddUserDataToView(model, breadCrumbs);

// ReSharper disable Asp.NotResolved
            return View(model);
// ReSharper restore Asp.NotResolved
        }

        /// <summary>
        /// Adds the user data to view.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="breadCrumbs">The bread crumbs.</param>
        private static void AddUserDataToView(object model, IEnumerable<KeyValuePair<string, string>> breadCrumbs)
        {
            IBaseModel baseInterface = model as IBaseModel;

            if (baseInterface != null)
            {
                RenderBreadCrumbs(baseInterface, breadCrumbs);
            }
        }

        /// <summary>
        /// Renders the bread crumbs.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="breadCrumbs">The bread crumbs.</param>
        private static void RenderBreadCrumbs(IBaseModel model, IEnumerable<KeyValuePair<string, string>> breadCrumbs)
        {
            List<BreadCrumb> crumbs = breadCrumbs.Select(keyValuePair => new BreadCrumb {Text = keyValuePair.Key, Href = keyValuePair.Value}).ToList();
            model.BreadCrumbs = crumbs;
        }

        /// <summary>
        /// Method called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Contains information about the current request and action.</param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            Owner = _userAuthorization.GetOwner(filterContext.HttpContext, filterContext.RouteData);
            UrlService = UserUrlService.GetInstance(Owner);
            base.OnAuthorization(filterContext);
        }

    }
}
