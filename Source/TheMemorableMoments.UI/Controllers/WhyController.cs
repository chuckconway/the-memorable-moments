using System.Web.Mvc;

namespace TheMemorableMoments.UI.Controllers
{
    public class WhyController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
// ReSharper disable Asp.NotResolved
            return View();
// ReSharper restore Asp.NotResolved
        }

    }
}
