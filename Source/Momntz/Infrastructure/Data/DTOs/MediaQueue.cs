namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class MediaQueue {
        public virtual long MediaQueueId { get; set; }
        public virtual int MediaId { get; set; }
        public virtual byte[] MediaBytes { get; set; }
        public virtual string Filename { get; set; }
    }
}
