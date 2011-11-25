using System.Web.Mvc;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;
using TheMemorableMoments.Infrastructure.Data.Queries;

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
                                                            scan.TheCallingAssembly();
                                                            scan.Assembly("TheMemorableMoments");
                                                            scan.WithDefaultConventions();
                                                            scan.AddAllTypesOf<IController>();
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