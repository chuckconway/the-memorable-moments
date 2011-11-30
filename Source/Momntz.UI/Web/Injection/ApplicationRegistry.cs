using Momntz.Infrastructure.Data.Queries;
using NHibernate;
using NHibernate.Cfg;
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
            For<ISessionFactory>()
                .Singleton()
                .Use(CreateSessionFactory());

            For<ISession>()
           .HttpContextScoped()
           .Use(context => context.GetInstance<ISessionFactory>().OpenSession());

            For<IInjection>().Use<StructureMapIoc>();
            For<IProjections>().Use<Projections>();
        }

        /// <summary>
        /// Creates the session factory.
        /// </summary>
        /// <returns></returns>
        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            configuration = configuration.Configure();
            configuration = configuration.AddAssembly(typeof(IQuery<>).Assembly);

            return  configuration.BuildSessionFactory();
        }
    }
}