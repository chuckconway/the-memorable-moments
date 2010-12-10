using System.Web.Mvc;
using Chucksoft.Core.Services;

namespace TheMemorableMoments.UI.Web.Controllers
{
    public class BaseController : Controller 
    {
        private readonly ICryptographyService _cryptographyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="cryptographyService">The cryptography service.</param>
        public BaseController(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController():this(DependencyInjection.Resolve<ICryptographyService>()) { }

        /// <summary>
        /// Gets or sets the site cookie.
        /// </summary>
        /// <value>The site cookie.</value>
        public SiteCookie SiteCookie
        {
            get
            {
                return new SiteCookie(HttpContext, _cryptographyService);
            }
        }
    }
}
