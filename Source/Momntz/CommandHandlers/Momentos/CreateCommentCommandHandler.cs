using Momntz.Commands.Momentos;
using Momntz.Domain.Model;
using Momntz.Infrastructure.Data.Command;
using Momntz.PubSub.Messages;
using NServiceBus;

namespace Momntz.CommandHandlers.Momentos
{
    public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand>
    {
        private readonly IDocumentDatabase _database;
        private readonly IBus _bus;

        /// <summary>
        /// Creates the user command handler.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="bus">The bus.</param>
        public CreateCommentCommandHandler(IDocumentDatabase database, IBus bus)
        {
            _database = database;
            _bus = bus;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(CreateCommentCommand command)
        {
            Momento momento = _database.SingleOrDefault<Momento>(c => c.Id == command.MomentoId);
            Comment comment = new Comment();
            comment.Author = comment.Author;
            comment.AuthorEmail = command.AuthorEmail;
            comment.AuthorUrl = command.AuthorUrl;
            comment.Body = comment.Body;
            comment.UserAgent = command.UserAgent;
            comment.UserIp = command.UserIp;

            momento.AddComment(comment);

            _database.Add(momento);
            _database.Save();

            CommentMessage commentMessage = ConvertToCommentMessage(comment);

            _bus.Send(commentMessage);
        }

        /// <summary>
        /// Converts to comment message.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        private CommentMessage ConvertToCommentMessage(Comment comment)
        {
            CommentMessage message = new CommentMessage
                                         {
                                             Author = comment.Author,
                                             AuthorEmail = comment.AuthorEmail,
                                             AuthorUrl = comment.AuthorUrl,
                                             Body = comment.Body,
                                             UserAgent = comment.UserAgent,
                                             UserIP = comment.UserIp
                                         };

            return message;

        }
    }
}
