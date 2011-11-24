using System.Web.Mvc;
using Chucksoft.Core.Services;

namespace TheMemorableMoments.UI.Controllers
{
    public class OopsController : Controller
    {
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OopsController"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        public OopsController(IConfigurationService configurationService)

        {
            _configurationService = configurationService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string valueByKey = _configurationService.GetValueByKey("rootUrl");
            ViewData["Url"] = valueByKey;
            return View();
        }

    }
}
