namespace TheMemorableMoments.Domain.Model
{
    public class PersistentCollection
    {
        /// <summary>
        /// Gets or sets the collection key.
        /// </summary>
        /// <value>The collection key.</value>
        public string CollectionKey { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the persistence.
        /// </summary>
        /// <value>The persistence.</value>
        public Persistence Persistence { get; set; }
    }

    public enum Persistence
    {
        Permanent,
        Temporary
    }
}