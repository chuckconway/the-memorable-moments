using Momntz.Infrastructure.Data.DTOs;
using Momntz.Infrastructure.Projections;
using NHibernate;

namespace Momntz.Infrastructure.Data.Queries.Home
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
                var items = _session.QueryOver<Momento>()
                            .Take(1)
                            .List<Momento>();

                _session.Transaction.Commit();
                return new HomeIndexProjection();
            }
            
        }
    }
}
