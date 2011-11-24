namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Location {
        public virtual string LocationName { get; set; }
        public virtual int UserId { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual byte Zoom { get; set; }
        public virtual string MapTypeId { get; set; }
    }
}
