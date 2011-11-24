using System.Collections.Generic;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class File {
        public File() {
			MediaFiles = new List<MediaFile>();
        }
        public virtual int FileId { get; set; }
        public virtual List<MediaFile> MediaFiles { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string FileExtension { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual long Size { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
    }
}
