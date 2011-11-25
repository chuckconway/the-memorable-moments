using NHibernate;
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
           var items = _session.QueryOver<Media>()
                .Take(90)
                .List<Media>();
            return new HomeIndexProjection() {Media = items};
        }
    }
}
