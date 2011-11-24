using System.Data;
using Hypersonic;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public abstract class RepositoryBase
    {
        protected IDatabase database;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        protected RepositoryBase()
        {
            database = new MsSqlDatabase {CommandType = CommandType.StoredProcedure};
        }
    }
}
