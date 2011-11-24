namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class WaitingList {
        public virtual int WaitingListId { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string CreateDate { get; set; }
    }
}
