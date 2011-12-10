using Momntz.Infrastructure.Data.DTOs;
using Momntz.Tests.Framework;
using NUnit.Framework;

namespace Momntz.Tests.Data.Integration
{
    [TestFixture]
    public class EmailTests : DbTestBase
    {
        [Test]
        public void Email_InsertEmail_IsInsertedSuccesfully()
        {
            PersistAndRetrieve<Email>(Sessions.Artifact);
        }
    }
}
