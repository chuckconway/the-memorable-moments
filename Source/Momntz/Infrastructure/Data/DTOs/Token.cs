namespace TheMemorableMoments.Infrastructure.Data.DTOs {
    
    public class Token {
        public virtual string Value { get; set; }
        public virtual string CreateDate { get; set; }
        public virtual bool IsValid { get; set; }
    }
}
