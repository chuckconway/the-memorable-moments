using Momntz.Infrastructure.Data.Queries;
using NHibernate;
using NHibernate.Cfg;

namespace Momntz.Infrastructure.Data
{
   public class DatabaseFactories : IMomntzSessionFactories
    {
        /// <summary>
        /// Gets the momntz session.
        /// </summary>
        /// <returns></returns>
        public ISessionFactory GetMomntzSession()
        {
            var configuration = new Configuration();
            configuration = configuration.Configure();
            configuration = configuration.AddAssembly(typeof(IQuery<>).Assembly);

            return configuration.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider")
            .SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver")
            .SetProperty("connection.connection_string", @"Server=Apollo;initial catalog=Dev.Momntz;Integrated Security=True")
            .SetProperty("dialect", "NHibernate.Dialect.MsSql2008Dialect")
            .BuildSessionFactory();
        }

        /// <summary>
        /// Gets the momntz session.
        /// </summary>
        /// <returns></returns>
        public ISessionFactory GetArtifactSession()
        {
            var configuration = new Configuration();
            configuration = configuration.Configure();
            configuration = configuration.AddAssembly(typeof(IQuery<>).Assembly);

            return configuration.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider")
            .SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver")
            .SetProperty("connection.connection_string", @"Server=Apollo;initial catalog=Dev.Artifact;Integrated Security=True")
            .SetProperty("dialect", "NHibernate.Dialect.MsSql2008Dialect")
            .BuildSessionFactory();
        }
    }
}
