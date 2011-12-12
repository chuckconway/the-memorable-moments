using Momntz.Infrastructure.Data;
using Momntz.Infrastructure.Data.Queries;
using Momntz.PubSub.Messages;
using NHibernate;
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
            ObjectFactory.Initialize(x =>
            {
                x.For<IMomntzSessionFactories>()
                    .Singleton()
                    .Use<DatabaseFactories>();

                x.For<IMomntzSessions>()
               .Use<Databases>();

                //x.AddRegistry(new ApplicationRegistry());
                //x.For<IStartableBus>().Use<Startable>();
                x.Scan(scan =>
                {
                    scan.AssemblyContainingType(typeof(IQuery<>));

                });
            });

            Configure.With()
            .StructureMapBuilder(ObjectFactory.Container)
            .XmlSerializer()
            .UnicastBus()
            .LoadMessageHandlers()
            .ImpersonateSender(false)
            .Sagas()
            .NHibernateSagaPersisterWithSQLiteAndAutomaticSchemaGeneration()
            .CreateBus()
            .Start();  
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
