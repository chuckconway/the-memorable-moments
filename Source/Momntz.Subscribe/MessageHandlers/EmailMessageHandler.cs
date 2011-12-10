using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Momntz.PubSub.Messages;
using NServiceBus;

namespace Momntz.Subscribe.MessageHandlers
{
    public class EmailMessageHandler : IHandleMessages<EmailMessage>
    {
        public void Handle(EmailMessage message)
        {
            AmazonSimpleEmailService client = new AmazonSimpleEmailServiceClient();
            SendEmailRequest email = new SendEmailRequest();
            email.Source = "chuck@cconway.com";
            email.Message = new Message(new Content("Welcome email"), new Body(new Content("Welcome test email")));
            email.Destination = new Destination(new List<string>{"chuck@cconway.com"});

            client.SendEmail(email);
        }
    }
}
