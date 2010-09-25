using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Services
{
    public class Authorization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authorization"/> class.
        /// </summary>
        /// <param name="isSignedIn">if set to <c>true</c> [is signed in].</param>
        /// <param name="isOwner">if set to <c>true</c> [is owner].</param>
        /// <param name="owner">The owner.</param>
        /// <param name="signedInUser">The signed in user.</param>
        public Authorization(bool isSignedIn, bool isOwner, User owner, User signedInUser )
        {
            IsOwner = isOwner;
            IsSignedIn = isSignedIn;
            Owner = owner;
            SignedInUser = signedInUser;
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public User Owner { get; set; }

        /// <summary>
        /// Gets or sets the signed in user.
        /// </summary>
        /// <value>The signed in user.</value>
        public User SignedInUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is signed in.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is signed in; otherwise, <c>false</c>.
        /// </value>
        public bool IsSignedIn { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owner.
        /// </summary>
        /// <value><c>true</c> if this instance is owner; otherwise, <c>false</c>.</value>
        public bool IsOwner { get; private set; }

    }
}