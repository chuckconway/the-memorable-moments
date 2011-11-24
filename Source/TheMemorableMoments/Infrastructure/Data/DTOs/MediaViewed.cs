namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class MediaViewed {
        public virtual int MediaViewedId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int MediaId { get; set; }
        public virtual string LastViewed { get; set; }
        public virtual int ViewCount { get; set; }
        public virtual string FirstViewed { get; set; }
    }
}
