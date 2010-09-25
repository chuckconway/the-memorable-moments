using System.Web.Mvc;

namespace TheMemorableMoments.UI.Web.Helpers
{
    public static class UIMessage
    {
        /// <summary>
        /// Messages the specified helper.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
       public static string Message(this HtmlHelper helper, string message)
       {
           return string.Format("<div style=\"display:none;\" ><span class=\"message\" >{0}</span></div>", message);
       }
    }
}