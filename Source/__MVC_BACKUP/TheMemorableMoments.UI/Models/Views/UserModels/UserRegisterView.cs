using System.ComponentModel.DataAnnotations;
using Chucksoft.Core.Services;
using TheMemorableMoments.UI.Web.Validation;

namespace TheMemorableMoments.UI.Models.Views.UserModels
{
    [PropertiesMustMatch("Password", "Confirm", ErrorMessage = "Doh! The passwords don't match!")]
    public class UserRegisterView 
    {
        private readonly ICryptographyService _cryptographyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegisterView"/> class.
        /// </summary>
        /// <param name="cryptographyService">The cryptography service.</param>
        public UserRegisterView(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegisterView"/> class.
        /// </summary>
        public UserRegisterView() : this(DependencyInjection.Resolve<ICryptographyService>()){}

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

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
        [Required(ErrorMessage = "Email is required")]
        [Email(ErrorMessage = "The email is in an incorrect format. ")]
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
        [StringLength(50, ErrorMessage = "password must have a minimum of 6 characters")]
        [Required(ErrorMessage = "Confirm password is required")]
        public string Confirm { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns></returns>
        public Domain.Model.User GetUser()
        {
            Domain.Model.User user = new Domain.Model.User
                                         {
                                             DisplayName = DisplayName,
                                             Email = Email,
                                             FirstName = FirstName,
                                             LastName = LastName,
                                             Password = _cryptographyService.Encrypt(Password),
                                             Username = Username
                                         };

            return user;
        }
    }
}


