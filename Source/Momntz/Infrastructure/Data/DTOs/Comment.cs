using System;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Comment {
        public virtual int CommentId { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string SiteUrl { get; set; }
        public virtual string Ip { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string CommentStatus { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime CommentDate { get; set; }
        public virtual Nullable<int> UserId { get; set; }
        public virtual Nullable<int> ParentId { get; set; }
    }
}
