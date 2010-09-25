using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.UI.Models;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web
{
    public class UserNavigationHelper
    {
        /// <summary>
        /// Gets the user navigation.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        /// <returns></returns>
        public static string GetUserNavigation(Authorization authorization)
        {
            string navigation;
            if (authorization.IsOwner)
            {
                const string format = @"
            <li><a href=""/{0}/dashboard"" >Dashboard</a></li>
            <li><a href=""/{0}/photos"" >Photos</a></li>
            <li><a href=""/{0}/albums"" >Albums</a></li>
            <li><a href=""/{0}/friends"" >Friends</a></li>
            <li><a href=""/{0}/account"" >Account</a></li>
            <li><a href=""/{0}/logoff"" >Sign Out</a></li>";

                navigation = string.Format(format, authorization.Owner.Username);
            }
            else if (authorization.IsSignedIn)
            {
                const string format = @"
            <li><a href=""/{0}/recent"" >Recent</a></li>
            <li><a href=""/{0}/tags"" >Tags</a></li>
            <li><a href=""/{0}/albums"" >Albums</a></li>
            <li><a href=""/{1}/logoff"" >Sign Out</a></li>";
                navigation = string.Format(format, authorization.Owner.Username, authorization.SignedInUser.Username);
            }
            else
            {
                const string format = @"
            <li><a href=""/{0}/recent"" >Recent</a></li>
            <li><a href=""/{0}/tags"" >Tags</a></li>
            <li><a href=""/{0}/albums"" >Albums</a></li>";
                navigation = string.Format(format, authorization.Owner.Username);
                navigation += SignInHtml();

            }
            return navigation; 
        }

        /// <summary>
        /// Signs the in HTML.
        /// </summary>
        /// <returns></returns>
        internal static string SignInHtml()
        {
            const string html = @"<li><a id=""signin"" href=""/login"" >Sign in</a></li>";
            return html;
        }

        /// <summary>
        /// Gets the breadcrumbs.
        /// </summary>
        /// <param name="keyValPair">The key val pair.</param>
        /// <returns></returns>
        public static string GetBreadCrumbs(IList<BreadCrumb> keyValPair)
        {
            const string userLink = "<label id=\"breadcrumbs\">";
            const string crumbLink = "{0}<span><a class=\"hyperlinks\" style=\"border-top:none;\" href=\"{1}\" >{2}</a></span> ";
            StringBuilder builder = new StringBuilder();

            if (keyValPair.Count > 0)
            {
                builder.AppendLine(string.Format(userLink));

                for (int index = 0; index < keyValPair.Count; index++)
                {
                    if (index == 0)
                    {
                        string link = string.Format(crumbLink, string.Empty, keyValPair[index].Href, keyValPair[index].Text);
                        builder.AppendLine(link);
                    }

                    if (!string.IsNullOrEmpty(keyValPair[index].Text) && index > 0)
                    {
                        string link = (!string.IsNullOrEmpty(keyValPair[index].Href)
                                           ? string.Format(crumbLink, "> ", keyValPair[index].Href,
                                                           keyValPair[index].Text)
                                           : string.Format("> <span>{0}</span>", keyValPair[index].Text));
                        builder.AppendLine(link);
                    }
                }

                builder.AppendLine("</label>");
            }
            return builder.ToString();
        }

    }
}
