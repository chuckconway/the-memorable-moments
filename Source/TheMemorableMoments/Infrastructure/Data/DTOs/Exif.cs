namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Exif {
        public virtual int MediaId { get; set; }
        public virtual string Key { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual string Value { get; set; }
        public virtual int Type { get; set; }
    }
}
