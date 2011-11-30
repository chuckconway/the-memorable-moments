namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class RecentActivity {
        public virtual int RecentActivityId { get; set; }
        public virtual string ActivityType { get; set; }
        public virtual int ActivityCount { get; set; }
        public virtual int UserId { get; set; }
        public virtual string CreateDate { get; set; }
    }
}
