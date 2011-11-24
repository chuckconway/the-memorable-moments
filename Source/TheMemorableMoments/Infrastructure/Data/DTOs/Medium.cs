using System.Collections.Generic;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Medium {
        public Medium() {
			AlbumMedias = new List<AlbumMedium>();
            Comments = new List<Comment>();
			Exifs = new List<Exif>();
			MediaFiles = new List<MediaFile>();
			TagMedias = new List<TagMedium>();
			Vieweds = new List<Viewed>();
        }
        public virtual int MediaId { get; set; }
        public virtual List<AlbumMedium> AlbumMedias { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Exif> Exifs { get; set; }
        public virtual List<MediaFile> MediaFiles { get; set; }
        public virtual List<TagMedium> TagMedias { get; set; }
        public virtual List<Viewed> Vieweds { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Status { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual string UploadStatus { get; set; }
    }
}
