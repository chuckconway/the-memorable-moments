namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Invitation {
        public virtual User User { get; set; }
        public virtual int InvitationId { get; set; }
        public virtual string Email { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual bool Sent { get; set; }
    }
}
