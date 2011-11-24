using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Models.Views
{
    public class DirectoryView : BaseModel
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public List<Member> Members { get; set; }
    }
}
