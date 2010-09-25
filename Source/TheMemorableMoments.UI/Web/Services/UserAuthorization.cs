using System;
using System.Web;
using System.Web.Routing;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Services
{
    public class UserAuthorization : IUserAuthorization
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthorization"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserAuthorization(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="routeData">The route data.</param>
        /// <returns></returns>
        public User GetOwner(HttpContextBase context, RouteData routeData)
        {
            User user = context.Items["username"] as User ?? GetOwner(routeData);
            return user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="routeData">The route data.</param>
        /// <returns></returns>
        private User GetOwner(RouteData routeData)
        {
            string username = Convert.ToString(routeData.Values["username"]);
            User user = _userRepository.RetrieveUserByUsername(username);
            return user;
        }
    }
}