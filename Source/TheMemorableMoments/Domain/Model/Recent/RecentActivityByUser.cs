using Hypersonic.Attributes;

namespace TheMemorableMoments.Domain.Model
{
    public class RecentActivityByUser : RecentActivity
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataAlias(Alias = "FriendId")]
        public int UserId { get; set; }
    }
}