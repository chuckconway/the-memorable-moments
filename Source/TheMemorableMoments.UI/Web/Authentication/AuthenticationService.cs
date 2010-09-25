using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Authentication
{
    public class AuthenticationService : IUserAuthentication
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserRepository _userRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public User User { get; private set; }


        /// <summary>
        /// Determines whether the specified username is valid.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// 	<c>true</c> if the specified username is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(string username, string password)
        {
            bool isValidUser = false;

            User user = _userRepository.RetrieveUserByLoginCredentials(username, password);

            if (user != null && user.Id > 0)
            {
                User = user;
                isValidUser = true;
            }

            return isValidUser;
        }
    }
}
