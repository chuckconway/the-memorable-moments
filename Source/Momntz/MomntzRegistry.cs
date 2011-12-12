using Momntz.Infrastructure;
using Momntz.Infrastructure.Data;
using Momntz.Infrastructure.Data.Command;
using Momntz.Infrastructure.Services.Spam;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace Momntz
{
    public class MomntzRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MomntzRegistry"/> class.
        /// </summary>
        public MomntzRegistry()
        {
            For<IMomntzSessionFactories>()
                .Singleton()
                .Use<DatabaseFactories>();

            For<ICommandProcessor>().Use<CommandProcessor>();
            For<IMomntzSessions>().HttpContextScoped().Use<Databases>();

            For<IDocumentDatabase>().Use<Infrastructure.Data.Command.RavenDb>();
            For<IDocumentStore>().Use(GetRavenDb());
            For<ICommentSpamService>().Use<CommentSpamService>();
        }

        /// <summary>
        /// Gets the raven db.
        /// </summary>
        /// <returns></returns>
        private static IDocumentStore GetRavenDb()
        {
            var documentStore = new DocumentStore { Url = "http://localhost:8080" };
            return documentStore.Initialize();
        }
    }
}
