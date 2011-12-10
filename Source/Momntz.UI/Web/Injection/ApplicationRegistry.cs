using Momntz.Infrastructure;
using Momntz.Infrastructure.Data;
using Momntz.Infrastructure.Data.Command;
using Momntz.Infrastructure.Data.Queries;
using NHibernate;
using NHibernate.Cfg;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace Momntz.UI.Web.Injection
{
    public class ApplicationRegistry : Registry 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRegistry"/> class.
        /// </summary>
        public ApplicationRegistry()
        {
            For<IMomntzSessionFactories>()
                .Singleton()
                .Use<DatabaseFactories>();

            For<ISession>()
           .HttpContextScoped()
           .Use(context => context.GetInstance<ISessionFactory>().OpenSession());

            For<IInjection>().Use<StructureMapIoc>();
            For<IProjections>().Use<Projections>();
            For<ICommandProcessor>().Use<CommandProcessor>();
            For<IMomntzSessions>().HttpContextScoped().Use<Databases>();

            For<IDocumentDatabase>().Use<Infrastructure.Data.Command.Raven>();
            For<IDocumentStore>().Use(GetRavenDb());

        }

        private static IDocumentStore GetRavenDb()
        {
            var documentStore = new DocumentStore { Url = "http://localhost:8080" };
            return documentStore.Initialize();
        }

    }
}