using System.Collections.Generic;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Album {
        public Album() {
            AlbumMedia = new List<AlbumMedium>();
        }
        public virtual int AlbumId { get; set; }
        public virtual User User { get; set; }
        public virtual List<AlbumMedium> AlbumMedia { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int? ParentId { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual string LastModifiedDate { get; set; }
        public virtual int? CoverMediaId { get; set; }
    }
}
