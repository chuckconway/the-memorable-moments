namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Friend {
        public virtual User FriendId_User { get; set; }
        public virtual User UserId_User { get; set; }
        public virtual string CreateDate { get; set; }
    }
}
