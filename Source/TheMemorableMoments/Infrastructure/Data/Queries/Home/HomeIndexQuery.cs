using System.Collections;
using NHibernate;
using Ploeh.AutoFixture;
using TheMemorableMoments.Infrastructure.Data.DTOs;
using TheMemorableMoments.Infrastructure.Projections;

namespace TheMemorableMoments.Infrastructure.Data.Queries.Home
{
    public class HomeIndexQuery : IQuery<HomeIndexProjection>
    {
        private readonly ISession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeIndexQuery"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public HomeIndexQuery(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Retrieves the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public HomeIndexProjection Retrieve(dynamic values)
        {
            using (_session.BeginTransaction())
            {
                //Fixture  fixture = new Fixture();
                //User user = fixture.CreateAnonymous<User>();
                //user.UserId = 0;

                //_session.Save(user);

                //var items = _session.CreateCriteria<Media>().List<Media>();

                var items = _session.QueryOver<Media>()
                            //.Where((m) => m.User.DisplayName == "Chuck")
                            .Take(1)
                            .List<Media>();

                _session.Transaction.Commit();
                return new HomeIndexProjection(); //{ Media = items };
            }
            
        }
    }
}
