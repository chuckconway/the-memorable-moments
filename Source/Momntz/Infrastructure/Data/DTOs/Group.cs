namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Group {
        public virtual int GroupId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual int CreatedBy { get; set; }
    }
}
