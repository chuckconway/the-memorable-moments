using Momntz.PubSub.Messages;
using NServiceBus;
using StructureMap;

namespace Momntz.Subscribe
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization, IWantToRunAtStartup
    {
        /// <summary>
        /// Gets or sets the bus.
        /// </summary>
        /// <value>
        /// The bus.
        /// </value>
        public IBus Bus { get; set; }

        /// <summary>
        /// Perform initialization logic.
        /// </summary>
        public void Init()
        {
            Configure.With()
            .StructureMapBuilder(ObjectFactory.Container)
            .XmlSerializer()
            .UnicastBus(); //managed by the class Subscriber2Endpoint
        }

        /// <summary>
        /// Method called at startup.
        /// </summary>
        public void Run()
        {
            Bus.Subscribe<ItemMessage>();
            Bus.Subscribe<EmailMessage>();
        }

        /// <summary>
        /// Method called on shutdown.
        /// </summary>
        public void Stop()
        {
            Bus.Unsubscribe<ItemMessage>();
            Bus.Unsubscribe<EmailMessage>();
        }
    }
}
