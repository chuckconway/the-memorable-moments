using System.ComponentModel.DataAnnotations;

namespace TheMemorableMoments.UI.Web.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailAttribute: RegularExpressionAttribute 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttribute"/> class.
        /// </summary>
        public EmailAttribute() : base(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$") { }
    }
}