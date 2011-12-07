using Momntz.PubSub.Messages;
using NServiceBus;

namespace Momntz.Subscribe.MessageHandlers
{
    public class ItemMessageHandler : IHandleMessages<ItemMessage>
    {
        public void Handle(ItemMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}
