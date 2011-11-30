using System;

namespace TheMemorableMoments.Infrastructure.Data.DTOs
{
    public class Media
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Tags { get; set; }
        public virtual int? MediaYear { get; set; }
        public virtual int? MediaMonth { get; set; }
        public virtual int? MediaDay { get; set; }
        public virtual int? CommentCount { get; set; }

        public virtual Visibility Visibility
        {
            get { return (Visibility)Enum.Parse(typeof(Visibility), Status); }
            set { Status = value.ToString(); }
        } 

        public virtual string Status { get; set; }
        public virtual User User { get; set; }
    }

    public enum Visibility
    {
        Public,
        Network,
        Private
    }
}
