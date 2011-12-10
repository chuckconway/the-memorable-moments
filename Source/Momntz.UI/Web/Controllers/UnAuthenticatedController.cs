using System.Web.Mvc;
using Momntz.UI.Web.ActionResults;

namespace Momntz.UI.Web.Controllers
{
    public class UnAuthenticatedController : Controller
    {
        /// <summary>
        /// Forms the specified form.
        /// </summary>
        /// <typeparam name="TForm">The type of the form.</typeparam>
        /// <param name="form">The form.</param>
        /// <param name="success">The success.</param>
        /// <returns></returns>
        protected FormActionResult<TForm> Form<TForm>(TForm form, ActionResult success)
        {
            var failure = View(form);
            return new FormActionResult<TForm>(form, success, failure);
        }

        /// <summary>
        /// Queries the specified view result.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="viewResult">The view result.</param>
        /// <returns></returns>
        protected ActionResult Query<TDestination>(ViewResult viewResult)
        {
            return viewResult;
        }
    }
}