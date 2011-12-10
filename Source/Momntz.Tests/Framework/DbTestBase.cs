using KellermanSoftware.CompareNetObjects;
using Momntz.Infrastructure.Data.DTOs;
using Momntz.Infrastructure.Data.Queries;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Momntz.Tests.Framework
{
    [TestFixture]
    public class DbTestBase
    {
        protected ISession MomntzSession;

        protected ISession ArtifactSession;

        /// <summary>
        /// Gets the session.
        /// </summary>
        [SetUp]
        public void GetSession()
        {
            //var configuration = new Configuration();
            //configuration = configuration.Configure();
            //configuration = configuration.AddAssembly(typeof(IQuery<>).Assembly);

            //MomntzSession = configuration.BuildSessionFactory().OpenSession();
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            MomntzSession = GetMomntzSession();
            ArtifactSession = GetArtifactSession();
        }

        /// <summary>
        /// Gets the momntz session.
        /// </summary>
        /// <returns></returns>
        private ISession GetMomntzSession()
        {
            var configuration = new Configuration();
            configuration = configuration.Configure();
            configuration = configuration.AddAssembly(typeof(IQuery<>).Assembly);

            return configuration.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider")
            .SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver")
            .SetProperty("connection.connection_string", @"Server=Apollo;initial catalog=Dev.Momntz;Integrated Security=True")
            .SetProperty("dialect", "NHibernate.Dialect.MsSql2008Dialect")
            .BuildSessionFactory().OpenSession();
        }

                /// <summary>
        /// Gets the momntz session.
        /// </summary>
        /// <returns></returns>
        private ISession GetArtifactSession()
        {
            var configuration = new Configuration();
            configuration = configuration.Configure();
            configuration = configuration.AddAssembly(typeof(IQuery<>).Assembly);

            return configuration.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider")
            .SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver")
            .SetProperty("connection.connection_string", @"Server=Apollo;initial catalog=Dev.Artifact;Integrated Security=True")
            .SetProperty("dialect", "NHibernate.Dialect.MsSql2008Dialect")
            .BuildSessionFactory().OpenSession();
        }

        /// <summary>
        /// Persists object the and retrieves the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        protected void PersistAndRetrieve<T>(ISession session) where T: class, IPrimaryKey<int>, new()
        {
            //Arrange
            Fixture fixture = new Fixture();
            var user = fixture.CreateAnonymous<T>();


            //Act
            session.Save(user);

            T user2 = session.QueryOver<T>()
                .Where((u) => u.Id == user.Id)
                .SingleOrDefault();


            //Assert
            CompareObjects compare = new CompareObjects();
            bool equal = compare.Compare(user, user2);
            Assert.IsTrue(equal);
        }

        [TearDown]
        public void CleanUp()
        {
            MomntzSession.Close();
            MomntzSession.Dispose();

            ArtifactSession.Close();
            ArtifactSession.Dispose();
        }
    }
}
