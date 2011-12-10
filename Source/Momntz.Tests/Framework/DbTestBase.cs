using System;
using KellermanSoftware.CompareNetObjects;
using Momntz.Infrastructure.Data;
using NHibernate;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Momntz.Tests.Framework
{
    [TestFixture]
    public class DbTestBase
    {
        protected IMomntzSessions Sessions;

        /// <summary>
        /// Gets the session.
        /// </summary>
        [SetUp]
        public void GetSession()
        {
            Sessions = new Databases(new DatabaseFactories());
            IntegrateWithNHProf();

        }

        private void IntegrateWithNHProf()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        /// <summary>
        /// Persists object the and retrieves the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        protected void PersistAndRetrieve<T>(ISession session)where T : class, IPrimaryKey<int>
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
            Sessions.Artifact.Close();
            Sessions.Artifact.Dispose();

            Sessions.Momntz.Close();
            Sessions.Momntz.Dispose();
        }
    }
}
