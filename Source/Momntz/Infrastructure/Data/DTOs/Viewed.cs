namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Viewed {
        public virtual int UserId { get; set; }
        public virtual int MediaId { get; set; }
        public virtual User User { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual string ViewedDateTime { get; set; }
    }
}
