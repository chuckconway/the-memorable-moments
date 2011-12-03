using Momntz.Infrastructure;
using Momntz.Infrastructure.Data.Queries;
using Momntz.UI.Web.Injection;

namespace Momntz.UI.Web
{
    public class Projections : IProjections
    {
        private readonly IInjection _injection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Projections"/> class.
        /// </summary>
        /// <param name="injection">The injection.</param>
        public Projections(IInjection injection)
        {
            _injection = injection;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return Get<T>(null);
        }

        /// <summary>
        /// Gets the specified values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public T Get<T>(dynamic values)
        {
            IQuery<T> query = _injection.Get<IQuery<T>>();
            return query.Retrieve(values);
        }
    }
}