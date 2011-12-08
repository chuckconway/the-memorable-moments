using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Momntz.Commands
{
    public class CreateUserCommand : ICommand<int>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public int Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommand"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        public CreateUserCommand(string firstName, string lastName, string username, string password, string email)
        {
            Id = 0;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
