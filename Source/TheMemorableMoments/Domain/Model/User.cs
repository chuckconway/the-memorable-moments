using Chucksoft.Core;
using Hypersonic.Attributes;

namespace TheMemorableMoments.Domain.Model
{
    public class User : IId<int> 
	{

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataAlias(Alias = "UserId")]
		public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
		public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
		public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
		public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
		public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
		public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
		public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
		public string Username { get; set; }

        /// <summary>
        /// Gets or sets the account status.
        /// </summary>
        /// <value>The account status.</value>
        public AccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public UserSettings Settings { get; set; }

	}

    public enum AccountStatus
    {
        Public,
        NetworkOnly,
        Private,
        Deleted
    }
}
