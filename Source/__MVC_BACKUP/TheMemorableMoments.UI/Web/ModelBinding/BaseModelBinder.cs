using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web.ModelBinding
{
    public class BaseModelBinder
    {
        private readonly IUserAuthorization _userAuthorization;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModelBinder"/> class.
        /// </summary>
        /// <param name="userAuthorization">The user authorization.</param>
        protected BaseModelBinder(IUserAuthorization userAuthorization)
        {
            _userAuthorization = userAuthorization;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected User GetUser(ControllerContext context)
        {
            return _userAuthorization.GetOwner(context.HttpContext, context.RouteData);
        }
    }
}