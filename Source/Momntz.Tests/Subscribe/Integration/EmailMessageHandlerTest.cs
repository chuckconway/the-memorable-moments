using System;
using System.Collections.Generic;
using Momntz.Infrastructure.Data.DTOs;
using Momntz.PubSub.Messages;
using Momntz.Subscribe.MessageHandlers;
using Momntz.Tests.Framework;
using NUnit.Framework;

namespace Momntz.Tests.Subscribe.Integration
{
    [TestFixture]
    public class EmailMessageHandlerTest : DbTestBase
    {
        [Test]
        public void EmailMessageHandler_SendEmail_EmailSuccessfullySent()
        {
            //Dependant on a template named 'welcome' in the Dev.Artifact.Template table.

            //Arrange
            User user = new User();
            user.FirstName = "Erin";
            user.Email = "donotreply@cconway.com";
            user.LastName = "Meraz";
            user.Password = "mango";
            user.Username = Guid.NewGuid().ToString();
            user.CreateDate = DateTime.UtcNow;

            Sessions.Momntz.Save(user);
            Sessions.Momntz.Flush();

            EmailMessage message = new EmailMessage(user.Id, "donotreply@cconway.com", "welcome", new Dictionary<string, string> { { "[FirstName]", "Erin" } });

            EmailMessageHandler handler = new EmailMessageHandler(Sessions);
            handler.Handle(message);
        }
    }
}
