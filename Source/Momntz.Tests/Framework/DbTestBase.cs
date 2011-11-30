using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Momntz.Infrastructure.Data.Queries;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Momntz.Tests.Framework
{
    [TestFixture]
    public class DbTestBase
    {
        protected ISession _session;

        [SetUp]
        public void GetSession()
        {
            var configuration = new Configuration();
            configuration = configuration.Configure();
            configuration = configuration.AddAssembly(typeof(IQuery<>).Assembly);

            _session = configuration.BuildSessionFactory().OpenSession();

            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        [TearDown]
        public void CleanUp()
        {
            _session.Close();
            _session.Dispose();
        }
    }
}
