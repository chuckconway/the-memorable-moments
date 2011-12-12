using Momntz.Commands;
using Momntz.Commands.User;
using Momntz.Domain.Model;
using Momntz.Exceptions;
using Momntz.Infrastructure.Data.Command;


namespace Momntz.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IDocumentDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeIndexCommandHandler"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public CreateUserCommandHandler(IDocumentDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(CreateUserCommand command)
        {
            var user = PopulateUser(command);
            var foundUser = CheckForDuplicateUsername(user);

            if(foundUser != null)
            {
                throw new DuplicateUsernameException(string.Format("Username '{0}' already exists.", user.Username));
            }

            _database.Add(user);
        }

        /// <summary>
        /// Checks for duplicate username.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private User CheckForDuplicateUsername(User user)
        {
            var foundUser = _database.SingleOrDefault<User>((u) => u.Username == user.Username);
            return foundUser;
        }

        /// <summary>
        /// Populates the user.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private static User PopulateUser(CreateUserCommand command)
        {
            return new User
                            {
                                AccountStatus = UserAccountStatus.Active,
                                Email = command.Email,
                                FirstName = command.FirstName,
                                LastName = command.LastName,
                                Password = command.Password,
                                Username = command.Username
                            };
        }
    }
}
