using System;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Media {
        public virtual int MediaLedgerId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Tags { get; set; }
        public virtual int? MediaYear { get; set; }
        public virtual int? MediaMonth { get; set; }
        public virtual int? MediaDay { get; set; }
        public virtual int MediaId { get; set; }
        public virtual string InsertDate { get; set; }
        public virtual int UserId { get; set; }
        public virtual string LocationName { get; set; }
    }
}
