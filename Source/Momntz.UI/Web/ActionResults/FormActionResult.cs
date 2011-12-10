using System.Web.Mvc;
using Momntz.UI.Web.Forms;

namespace Momntz.UI.Web.ActionResults
{
    public class FormActionResult<T> : ActionResult
    {
        /// <summary>
        /// Gets the failure.
        /// </summary>
        public ViewResult Failure { get; private set; }

        /// <summary>
        /// Gets the success.
        /// </summary>
        public ActionResult Success { get; private set; }

        /// <summary>
        /// Gets the form.
        /// </summary>
        public T Form { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormActionResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="success">The success.</param>
        /// <param name="failure">The failure.</param>
        public FormActionResult(T form, ActionResult success, ViewResult failure)
        {
            Form = form;
            Success = success;
            Failure = failure;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (!context.Controller.ViewData.ModelState.IsValid)
            {
                Failure.ExecuteResult(context);
            }
            else
            {
                var handler = DependencyResolver.Current.GetService<IFormHandler<T>>();
                handler.Handle(Form);

                Success.ExecuteResult(context);
            }
        }
    }
}