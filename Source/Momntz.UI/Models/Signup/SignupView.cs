using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Momntz.UI.Models.Signup
{
    public class SignupView
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [StringLength(250)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [StringLength(250)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm.
        /// </summary>
        /// <value>
        /// The confirm.
        /// </value>
        [StringLength(250)]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}