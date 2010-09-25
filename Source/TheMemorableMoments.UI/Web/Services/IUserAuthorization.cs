using System.Web;
using System.Web.Routing;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Services
{
    public interface IUserAuthorization
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="routeData">The route data.</param>
        /// <returns></returns>
        User GetOwner(HttpContextBase context, RouteData routeData);
    }
}