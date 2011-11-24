using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Chucksoft.Core.Web.CacheProviders;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Web.Cache;

namespace TheMemorableMoments.UI.Web
{
    public class UserRouteConstraint : IRouteConstraint 
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheProvider _cacheProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRouteConstraint"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="cacheProvider">The cache provider.</param>
        public UserRouteConstraint(IUserRepository userRepository, ICacheProvider cacheProvider)
        {
            _userRepository = userRepository;
            _cacheProvider = cacheProvider;
        }

        /// <summary>
        /// Determines whether the URL parameter contains a valid value for this constraint.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <param name="route">The object that this constraint belongs to.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        /// <param name="values">An object that contains the parameters for the URL.</param>
        /// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is being handled or when a URL is being generated.</param>
        /// <returns>
        /// true if the URL parameter contains a valid value; otherwise, false.
        /// </returns>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            UserCache userCache = new UserCache(_userRepository, _cacheProvider);
            IList<User> users = userCache.SelectAll();

            return (from user in users
                    from keyValuePair in values
                    where user.Username.Equals(keyValuePair.Value.ToString(), StringComparison.InvariantCultureIgnoreCase)
                    select user).Any();
        }
    }
}
