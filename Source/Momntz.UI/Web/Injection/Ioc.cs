using System.Web.Mvc;
using Momntz.CommandHandlers;
using Momntz.Infrastructure.Data.Queries;
using NServiceBus;
using NServiceBus.Serializers.XML;
using NServiceBus.Unicast.Transport;
using StructureMap;

namespace Momntz.UI.Web.Injection
{
    public static class IoC
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        public static IContainer Container { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new ApplicationRegistry());
                                             x.AddRegistry(new MomntzRegistry());

                                             x.Scan(scan =>
                                                        {
                                                            //scan.WithDefaultConventions();
                                                            //scan.TheCallingAssembly();
                                                            scan.AddAllTypesOf<IController>();
                                                            scan.AssemblyContainingType(typeof (IQuery<>));
                                                            scan.AddAllTypesOf(typeof (IQuery<>));
                                                            scan.AddAllTypesOf(typeof(ICommandHandler<>));
                                                        });
                                         });


            
            Container = ObjectFactory.Container;
            RegisterNServiceBus();

            return Container;
        }

        /// <summary>
        /// Gets the service bus.
        /// </summary>
        /// <returns></returns>
        private static void RegisterNServiceBus()
        {
                Configure
                .WithWeb()
                .Log4Net()
                .StructureMapBuilder(ObjectFactory.Container)
                .XmlSerializer()
                .MsmqTransport()
                .IsTransactional(true)
                .PurgeOnStartup(false)
                .UnicastBus()
                .ImpersonateSender(false)
                .CreateBus()
                .Start();

        }

        /// <summary>
        /// Releases the and dispose all HTTP scoped objects.
        /// </summary>
        public static void ReleaseAndDisposeAllHttpScopedObjects()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}