using System;

namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Configuration {
        public virtual Guid ConfigurationId { get; set; }
        public virtual string SiteName { get; set; }
        public virtual string SiteTagLine { get; set; }
        public virtual string Membership { get; set; }
        public virtual short ThumbNailHeight { get; set; }
        public virtual short ThumbNailWidth { get; set; }
        public virtual short WebHeight { get; set; }
        public virtual short WebWidth { get; set; }
    }
}
