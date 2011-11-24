using System;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class UploadBatch {
        public virtual Guid BatchId { get; set; }
        public virtual string LocationName { get; set; }
        public virtual decimal? Latitude { get; set; }
        public virtual decimal? Longitude { get; set; }
        public virtual int? Zoom { get; set; }
        public virtual string MapTypeId { get; set; }
        public virtual int? Year { get; set; }
        public virtual int? Month { get; set; }
        public virtual int? Day { get; set; }
        public virtual string Albums { get; set; }
        public virtual string Tags { get; set; }
        public virtual string MediaStatus { get; set; }
    }
}
