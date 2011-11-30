using System.Collections.Generic;
using Momntz.Infrastructure.Data.DTOs;
using Momntz.Tests.Framework;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Momntz.Tests.Integration.Data
{
    [TestFixture]
    public class MediaTests: DbTestBase
    {
        [Test]
        public void MomentoObjectGraph_PersistGEt_ResultsSuccessfullyRetrieved()
        {
            //Arrange
            var fixture = new Fixture();
            var user = fixture.CreateAnonymous(new User() { Id = 0 });
            var momento = new Momento { Id = 0, User = user, Name = "Chuckj was here", Visibility = Visibility.Public, Tags = new List<Tag>{new Tag {Id = 0, Name = "chuck-conway", Type = "Tag"}}};

            var item = new Item { Id = 0, Momento = momento, Name = "Thumbnail", OriginalName = "Chuck1", Extension = ".pdf"};
            momento.Items = new List<Item> { item };

            var facet = new Facet() { Id = 0, Name = "Chuck", Value = "Conway",Item = item };
            item.Facets = new List<Facet> {facet};
            
            //Act
            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(momento);
                tx.Commit();
            }

            IList<Facet> list = _session.QueryOver<Facet>()
                .Take(10)
                .List<Facet>();

            //Assert
            Assert.Greater(list.Count, 0);
        }
    }
}
