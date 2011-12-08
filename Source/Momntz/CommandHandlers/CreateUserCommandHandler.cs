using Momntz.Commands;
using Momntz.Infrastructure.Data.Command;
using Momntz.Infrastructure.Data.DTOs;

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
            User user = new User
                            {
                                AccountStatus = UserAccountStatus.Active,
                                Email = command.Email,
                                FirstName = command.FirstName,
                                LastName = command.LastName,
                                Password = command.Password,
                                Username = command.Username
                            };

            _database.Add(user);
        }
    }
}
