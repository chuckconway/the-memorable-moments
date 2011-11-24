using System.Web.Mvc;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web.Controls
{
    public static class NavigationHtmlHelper
    {
        /// <summary>
        /// Nons the signed in menu.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns></returns>
        public static string NonSignedInMenu(this HtmlHelper helper)
        {
          string navigation =
                 @"<li><a href=""/about"" title=""about the site"">about</a></li>
                <li><a href=""/directory"" title=""view our member directory"">directory</a></li>
                <li><a href=""/join"" title=""you want to contribute?"">join</a></li>" +
                UserNavigationHelper.SignInHtml();
            return navigation;
        }

        /// <summary>
        /// Sites the navigation.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="authorization">The authorization.</param>
        /// <returns></returns>
        public static string SiteNavigation(this HtmlHelper helper, Authorization authorization)
        {
            string navigation = UserNavigationHelper.GetUserNavigation(authorization);
            return navigation;
        }


    }
}