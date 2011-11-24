using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Models.Views.Friends
{
    public class FriendsView : BaseModel
    {
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; set; }

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

        /// <summary>
        /// Gets or sets the friends count.
        /// </summary>
        /// <value>The friends count.</value>
        public int FriendsCount { get; set; }

    }
}


