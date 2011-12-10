using Momntz.Infrastructure.Data.DTOs;
using Momntz.Tests.Framework;
using NUnit.Framework;

namespace Momntz.Tests.Data.Integration
{
    public class UserTests : DbTestBase
    {
        [Test]
        public void User_EntitySave_ThatTheUserIsSuccessfullySavedAndRetrieved()
        {
            PersistAndRetrieve<User>(Sessions.Momntz);
        }
    }
}
