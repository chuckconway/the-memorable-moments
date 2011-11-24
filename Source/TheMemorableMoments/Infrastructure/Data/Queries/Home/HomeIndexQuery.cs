using System;
using ChuckConway.Cqrs.Infrastructure;
using TheMemorableMoments.Infrastructure.Projections;

namespace TheMemorableMoments.Infrastructure.Data.Queries.Home
{
    public class HomeIndexQuery : IQuery<HomeIndexProjection>
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeIndexQuery"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public HomeIndexQuery(IRepository repository)
        {
            _repository = repository;
        }

        public HomeIndexProjection Retrieve(dynamic values)
        {
            //_repository.Get<>()
            return new HomeIndexProjection();
        }
    }
}
