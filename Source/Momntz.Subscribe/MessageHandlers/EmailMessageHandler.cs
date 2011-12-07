using System;

using Momntz.PubSub.Messages;
using NServiceBus;

namespace Momntz.Subscribe.MessageHandlers
{
    public class EmailMessageHandler : IHandleMessages<EmailMessage>
    {
        public void Handle(EmailMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
