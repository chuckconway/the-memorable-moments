using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Authentication
{
    public interface IUserAuthentication
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        User User { get; }

        /// <summary>
        /// Determines whether the specified username is valid.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="blogId">The blog id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified username is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid(string username, string password);
    }
}
