using System.Collections.Generic;
using Hypersonic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.Infrastructure.DataAccess
{
    public interface IQueryable<T>
    {
        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        List<T> Execute<P>(P parameters);
    }

    public class Queryable : IQueryable<User>
    {
        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public List<User> Execute<P>(P parameters)
        {
            //IDatabase database = new MsSqlDatabase();
            //database.
        }
    }
}
