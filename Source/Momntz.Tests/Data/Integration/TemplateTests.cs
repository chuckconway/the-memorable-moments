using KellermanSoftware.CompareNetObjects;
using Momntz.Infrastructure.Data.DTOs;
using Momntz.Tests.Framework;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Momntz.Tests.Data.Integration
{
    [TestFixture]
    public class TemplateTests : DbTestBase
    {
        [Test]
        public void Template_InsertTemplate_SucessfullyInserted()
        {
            //Arrange
            Fixture fixture = new Fixture();
            var temp = fixture.CreateAnonymous<Template>();

            //Template temp = new Template
            //                    {
            //                        Name = "welcome",
            //                        Body = "Dear [FirstName], I'm glad you could join our family. Best Regards, Chuck Conway",
            //                        Title = "Welcome to Momntz"
            //                    };


            //Act
            Sessions.Artifact.Save(temp);
            Sessions.Artifact.Flush();

            Template temp2 = Sessions.Artifact.QueryOver<Template>()
                .Where((u) => u.Name == temp.Name)
                .SingleOrDefault();


            //Assert
            CompareObjects compare = new CompareObjects();
            bool equal = compare.Compare(temp, temp2);
            Assert.IsTrue(equal);
        }
    }
}
