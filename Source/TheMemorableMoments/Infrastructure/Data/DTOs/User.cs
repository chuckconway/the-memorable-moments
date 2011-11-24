using System.Collections.Generic;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class User {
        public User() {
			Albums = new List<Album>();
			Friends = new List<Friend>();
			Invitations = new List<Invitation>();
            Viewed = new List<Viewed>();
        }
        public virtual int UserId { get; set; }
        public virtual List<Album> Albums { get; set; }
        public virtual List<Friend> Friends { get; set; }
        public virtual List<Invitation> Invitations { get; set; }
        public virtual List<Viewed> Viewed { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual string Username { get; set; }
        public virtual string LastLogin { get; set; }
        public virtual string CurrentSession { get; set; }
        public virtual string AccountStatus { get; set; }
        public virtual bool EnableReceivingOfEmails { get; set; }
        public virtual short WebViewMaxWidth { get; set; }
        public virtual short WebViewMaxHeight { get; set; }
        public virtual byte MaxInvitations { get; set; }
    }
}
