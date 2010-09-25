using System.Web.Mvc;

namespace TheMemorableMoments.UI.Controllers
{
    public class AboutController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
