using System.Collections.Generic;
using System.Linq;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Momntz.Infrastructure.Data;
using Momntz.Infrastructure.Data.DTOs;
using Momntz.PubSub.Messages;
using NServiceBus;

namespace Momntz.Subscribe.MessageHandlers
{
    public class EmailMessageHandler : IHandleMessages<EmailMessage>
    {
        private readonly IMomntzSessions _sessions;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageHandler"/> class.
        /// </summary>
        /// <param name="sessions">The sessions.</param>
        public EmailMessageHandler(IMomntzSessions sessions)
        {
            _sessions = sessions;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(EmailMessage message)
        {
            var template = _sessions.Artifact.Get<Template>(message.Template);
            var user = _sessions.Momntz.Get<User>(message.UserId);
            
            string title = message.Tokens.Aggregate(template.Title, (current, token) => current.Replace(token.Key, token.Value));
            string body = message.Tokens.Aggregate(template.Body, (current, token) => current.Replace(token.Key, token.Value));

            AmazonSimpleEmailService client = new AmazonSimpleEmailServiceClient();
            SendEmailRequest email = new SendEmailRequest
                                         {
                                             Source = message.From,
                                             Message = new Message(new Content(title), new Body(new Content(body))),
                                             Destination = new Destination(new List<string> {user.Email})
                                         };

            client.SendEmail(email);
            PersistEmail(message, email);
        }

        /// <summary>
        /// Persists the email.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="email">The email.</param>
        private void PersistEmail(EmailMessage message, SendEmailRequest email)
        {
            var persistedEmail = new Email
                                     {
                                         Body = email.Message.Body.Text.Data,
                                         From = message.From,
                                         Subject = email.Message.Subject.Data,
                                         To = email.Destination.ToAddresses.Aggregate(string.Concat)
                                     };

            _sessions.Artifact.Save(persistedEmail);
        }
    }
}
