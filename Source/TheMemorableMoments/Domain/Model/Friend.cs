using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Model
{
    public class Friend 
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int FriendId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the photo count.
        /// </summary>
        /// <value>The photo count.</value>
        public int PhotoCount { get; set; }

        /// <summary>
        /// Gets or sets the tag count.
        /// </summary>
        /// <value>The tag count.</value>
        public int TagCount { get; set; }

        /// <summary>
        /// Gets or sets the friends.
        /// </summary>
        /// <value>The friends.</value>
        public int FriendCount { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public Media Media { get; set; }
    }
}
