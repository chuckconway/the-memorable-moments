using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments
{
    public interface IPersistentCollectionRepository
    {
        /// <summary>
        /// Sets the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        void Set(PersistentCollection collection);

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="collectionKey">The collection key.</param>
        /// <returns></returns>
        PersistentCollection Get(string collectionKey);
    }
}