using Momntz.Commands.Momentos;
using Momntz.Domain.Model;
using Momntz.Infrastructure.Data.Command;

namespace Momntz.CommandHandlers.Momentos
{
    public class MarkCommentAsSpamCommandHandler : ICommandHandler<MarkCommentAsSpamCommand>
    {
        private readonly IDocumentDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkCommentAsSpamCommandHandler"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public MarkCommentAsSpamCommandHandler(IDocumentDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(MarkCommentAsSpamCommand command)
        {
            Momento momento = _database.SingleOrDefault<Momento>((m) => m.Id == command.MomentoId);
            momento.MarkCommentAsSpam(command.CommentId);

            _database.Add(momento);
        }
    }
}
