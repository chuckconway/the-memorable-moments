using KellermanSoftware.CompareNetObjects;
using Momntz.Infrastructure.Data.DTOs;
using Momntz.Tests.Framework;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Momntz.Tests.Integration.Data
{
    public class UserTests: DbTestBase
    {
        [Test]
        public void User_EntitySave_ThatTheUserIsSuccessfullySavedAndRetrieved()
        {
            //Arrange
            Fixture fixture = new Fixture();
            var user = fixture.CreateAnonymous<User>();
           // user.Id = 0; //reset the value to zero so nHibernate will do an insert.


            //Act
            _session.Save(user);
            User user2 = _session.QueryOver<User>()
                .Where((u) => u.Id == user.Id)
                .SingleOrDefault();


            //Assert
            CompareObjects compare = new CompareObjects();
            bool equal = compare.Compare(user, user2);
            Assert.IsTrue(equal);
        }
    }
}
