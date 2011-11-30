using System.Collections.Generic;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Tag {
        public Tag() {
			TagMedia = new List<TagMedium>();
        }
        public virtual int TagId { get; set; }
        public virtual List<TagMedium> TagMedia { get; set; }
        public virtual string TagName { get; set; }
        public virtual string Description { get; set; }
    }
}
