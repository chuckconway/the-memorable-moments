using System;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class MediaUploadBatch {
        public virtual Guid UploadBatch { get; set; }
        public virtual int MediaId { get; set; }
        public virtual string CreateDate { get; set; }
    }
}
