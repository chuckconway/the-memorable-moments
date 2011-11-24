using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web.Controllers
{
    public class HybridController : AnonymousController
    {
        private readonly IUserRepository _repository;
        private const string _signedOnUser = "SignedOnUser";

        /// <summary>
        /// Initializes a new instance of the <see cref="HybridController"/> class.
        /// </summary>
        public HybridController(): base(DependencyInjection.Resolve<IUserAuthorization>())
        {
            _repository = DependencyInjection.Resolve<IUserRepository>();
        }
        /// <summary>
        /// Gets the security information for the current HTTP request.
        /// </summary>
        /// <value></value>
        public User SignedInUser
        {
            get
            {
                User user = HttpContext.Items[_signedOnUser] as User ?? SetUser();
                return user;
            }
        }

        /// <summary>
        /// Models the factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T ModelFactory<T>()
        {
            T factory = base.ModelFactory<T>();
            factory.Authorization = Authorization;

            return factory;
        }

        /// <summary>
        /// Sets the Authorization and UrlService on the Model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T SetAuthorizationAndUrlService<T>(T instance)
        {
            instance.Authorization = Authorization;
            instance.UrlService = UrlService;

            return instance;
        }

        /// <summary>
        /// Method called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Contains information about the current request and action.</param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            SetUser();
        }

        /// <summary>
        /// Gets the authorization.
        /// </summary>
        /// <returns></returns>
        protected override Authorization GetAuthorization()
        {
            return Authorization;
        }

        /// <summary>
        /// Gets or sets the authorization.
        /// </summary>
        /// <value>The authorization.</value>
        public Authorization Authorization { get; private set; }

        /// <summary>
        /// Sets the user.
        /// </summary>
        private User SetUser()
        {
            User user = null;
            HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null)
            {
                user = SetUserToContext(cookie);
            }
            else
            {
               Authorization = new Authorization(false, false, Owner, null); 
            }

            return user;
        }

        /// <summary>
        /// Sets the user to context.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        protected User SetUserToContext(HttpCookie cookie)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            int userId = Convert.ToInt32(ticket.Name);
            User user = _repository.RetrieveByPrimaryKey(userId);
            
            bool isSignedIn = (user != null);
            bool isOwner = (user != null && Owner.Id == user.Id);
            
            if (user != null)
            {
                HttpContext.Items[_signedOnUser] = user;
            }

            Authorization = new Authorization(isSignedIn, isOwner, Owner, SignedInUser);
            return user;
        }
    }
}
