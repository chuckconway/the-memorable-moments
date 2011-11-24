using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Models.Views.Friends
{
    /// <summary>
    /// 
    /// </summary>
    public class FriendAllView : BaseModel
    {

        /// <summary>
        /// Gets or sets the friends.
        /// </summary>
        /// <value>The friends.</value>
        public List<Friend> Friends { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

    }
}


