namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class TagMedium {
        public virtual int TagId { get; set; }
        public virtual int MediaId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual string CreateDate { get; set; }
    }
}
