using System.ComponentModel.DataAnnotations;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Web.Validation;

namespace TheMemorableMoments.UI.Models.Views
{
    [PropertiesMustMatch("Password", "Confirm", ErrorMessage = "Doh! The passwords don't match!")]
    public class AccountDetailsView : BaseModel
    {

        /// <summary>
        /// Populates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Populate(User user)
        {
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            DisplayName = user.DisplayName;
            Email = user.Email;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDetailsView"/> class.
        /// </summary>
        public AccountDetailsView(){}

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            User user = new User
                            {
                                DisplayName = DisplayName,
                                Email = Email,
                                FirstName = FirstName,
                                LastName = LastName,
                                Password = Password,
                                Username = Username
                            };
            return user;
        }


        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name can not exceed 50 characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name can not exceed 50 characters")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required(ErrorMessage = "Display name is required")]
        [StringLength(100, ErrorMessage = "Display name can not exceed 100 characters")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Email(ErrorMessage = "The email is in an incorrect format. ")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [ValidateMinimumLength(6)]
        [StringLength(50, ErrorMessage = "Password can not exceed 50 characters")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm.
        /// </summary>
        /// <value>The confirm.</value>
        [StringLength(50, ErrorMessage = "Password can not exceed 50 characters")]
        [Required(ErrorMessage = "Confirm password is required")]
        public string Confirm { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }


        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
    }
}
