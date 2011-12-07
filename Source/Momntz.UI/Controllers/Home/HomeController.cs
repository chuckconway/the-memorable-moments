using System;
using System.Web.Mvc;
using Momntz.Commands;
using Momntz.Infrastructure;
using Momntz.Infrastructure.Projections;
using Momntz.UI.Web;

namespace Momntz.UI.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IProjections _projections;
        private readonly ICommandProcessor _commandProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="projections">The projections.</param>
        /// <param name="commandProcessor">The command processor.</param>
        public HomeController(IProjections projections, ICommandProcessor commandProcessor)
        {
            _projections = projections;
            _commandProcessor = commandProcessor;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        
        //public ActionResult Index()
        //{
        //    var projection = _projections.Get<HomeIndexProjection>();
        //    return View(projection);
        //}

        /// <summary>
        /// Indexes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            HomeIndexCommand command = new HomeIndexCommand(Guid.NewGuid());
            _commandProcessor.Process(command);
            return View();
        }

    }
}
