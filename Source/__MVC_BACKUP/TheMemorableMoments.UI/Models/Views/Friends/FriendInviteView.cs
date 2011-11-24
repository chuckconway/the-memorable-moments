namespace TheMemorableMoments.UI.Models.Views.Friends
{
    public class FriendInviteView : BaseModel
    {

        /// <summary>
        /// Gets or sets the friends count.
        /// </summary>
        /// <value>The friends count.</value>
        public int FriendsCount { get; set; }

        /// <summary>
        /// Gets or sets the UI message.
        /// </summary>
        /// <value>The UI message.</value>
        public string UIMessage { get; set; }

        /// <summary>
        /// Gets or sets the remaining invitations count.
        /// </summary>
        /// <value>The remaining invitations count.</value>
        public byte RemainingInvitationsCount { get; set; }
    }
}


