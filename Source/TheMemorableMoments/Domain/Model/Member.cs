using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Model
{
    public class Member
    {
        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public Media Media { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public User User { get; set; }
    }
}
