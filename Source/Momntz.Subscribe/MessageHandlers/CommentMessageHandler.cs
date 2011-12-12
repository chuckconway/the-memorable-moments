using System;
using Momntz.Infrastructure.Data;
using Momntz.Infrastructure.Services.Spam;
using Momntz.PubSub.Messages;
using NServiceBus;

namespace Momntz.Subscribe.MessageHandlers
{
    public class CommentMessageHandler : IHandleMessages<CommentMessage>
    {
        private readonly IMomntzSessions _sessions;
        private readonly ICommentSpamService _spamService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentMessageHandler"/> class.
        /// </summary>
        /// <param name="sessions">The sessions.</param>
        /// <param name="spamService">The spam service.</param>
        public CommentMessageHandler(IMomntzSessions sessions, ICommentSpamService spamService)
        {
            _sessions = sessions;
            _spamService = spamService;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(CommentMessage message)
        {
            CheckCommentForSpam spam = ConvertToCheckCommentForSpam(message);
            bool isSpam = _spamService.IsSpam(spam);


        }

        /// <summary>
        /// Converts to check comment for spam.
        /// </summary>
        /// <param name="message">The message.</param>
        private static CheckCommentForSpam ConvertToCheckCommentForSpam(CommentMessage message)
        {
            return new CheckCommentForSpam
                                           {
                                               Author = message.Author,
                                               AuthorEmail = message.AuthorEmail,
                                               AuthorUrl = message.AuthorUrl,
                                               Body = message.Body,
                                               UserAgent = message.UserAgent,
                                               UserIP = message.UserIP
                                           };
        }
    }
}
