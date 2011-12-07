using Momntz.Commands;
using Momntz.Infrastructure.Data.Command;
using Momntz.Infrastructure.Data.DTOs;

namespace Momntz.CommandHandlers
{
    public class HomeIndexCommandHandler : ICommandHandler<HomeIndexCommand>
    {
        private readonly IDocumentDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeIndexCommandHandler"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public HomeIndexCommandHandler(IDocumentDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(HomeIndexCommand command)
        {
            User momento = new User();
            //momento.Visibility = Visibility.Private;
            _database.Add(momento);
            _database.Save();
        }
    }
}
