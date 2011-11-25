using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Momntz.UI.Web;
using Momntz.UI.Web.Injection;
using TheMemorableMoments.Infrastructure.Projections;

namespace Momntz.UI.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IProjections _projections;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="projections">The projections.</param>
        public HomeController(IProjections projections)
        {
            _projections = projections;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var projection = _projections.Get<HomeIndexProjection>();
            return View(projection);
        }

    }
}
