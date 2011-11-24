using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class PersistentCollectionRepository : RepositoryBase, IPersistentCollectionRepository
    {
        /// <summary>
        /// Sets the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void Set(PersistentCollection collection)
        {
            database.NonQuery("PersistentCollection_Set", collection);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="collectionKey">The collection key.</param>
        /// <returns></returns>
        public PersistentCollection Get(string collectionKey)
        {
            return database.PopulateItem("PersistentCollection_Get", new { collectionKey },
                                         database.AutoPopulate<PersistentCollection>);
        }
    }
}
