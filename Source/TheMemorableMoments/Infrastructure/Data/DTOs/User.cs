namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class User {
        public virtual int UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
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
