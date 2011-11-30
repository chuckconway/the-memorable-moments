using System.Web.Mvc;
using Momntz.Infrastructure.Data.Queries;
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
                                             x.Scan(scan =>
                                                        {
                                                            scan.WithDefaultConventions();
                                                            scan.TheCallingAssembly();
                                                            scan.AddAllTypesOf<IController>();
                                                            scan.AssemblyContainingType(typeof (IQuery<>));
                                                            scan.AddAllTypesOf(typeof (IQuery<>));
                                                        });
                                         });
            
            Container = ObjectFactory.Container;
            return Container;
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