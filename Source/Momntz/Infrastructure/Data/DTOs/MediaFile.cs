namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class MediaFile {
        public virtual int MediaId { get; set; }
        public virtual int FileId { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual File File { get; set; }
        public virtual string MediaFormat { get; set; }
        public virtual string MediaType { get; set; }
    }
}
