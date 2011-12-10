using KellermanSoftware.CompareNetObjects;
using Momntz.Infrastructure.Data.DTOs;
using Momntz.Tests.Framework;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Momntz.Tests.Integration.Data
{
    public class UserTests : DbTestBase
    {
        [Test]
        public void User_EntitySave_ThatTheUserIsSuccessfullySavedAndRetrieved()
        {
            PersistAndRetrieve<User>(MomntzSession);
        }
    }
}
