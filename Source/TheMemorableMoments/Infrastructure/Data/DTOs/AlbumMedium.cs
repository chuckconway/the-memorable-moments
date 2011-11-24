namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class AlbumMedium {
        public virtual int AlbumId { get; set; }
        public virtual int MediaId { get; set; }
        public virtual Album Album { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual System.Nullable<int> Position { get; set; }
    }
}
