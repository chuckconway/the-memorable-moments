namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class PersistentCollection {
        public virtual string CollectionKey { get; set; }
        public virtual string Value { get; set; }
        public virtual string Persistence { get; set; }
        public virtual string CreateDate { get; set; }
    }
}
