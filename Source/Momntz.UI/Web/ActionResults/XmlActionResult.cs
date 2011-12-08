using System.Web.Mvc;
using Chucksoft.Core.Extensions;

namespace Momntz.UI.Web.ActionResults
{
    public class XmlResult<T> : ActionResult
    {
        /// <summary>
        /// Gets or sets the model to serialize.
        /// </summary>
        /// <value>
        /// The model to serialize.
        /// </value>
        public T ModelToSerialize { get; set; }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            var contentResult = new ContentResult {Content = ModelToSerialize.Serialize(), ContentType = "text/xml"};
            contentResult.ExecuteResult(context);
        }
    }
}