using Hypersonic.Attributes;

namespace TheMemorableMoments.Domain.Model.MediaClasses
{
    public class Owner
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [IgnoreParameter]
        public string Username {get; set;}

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [IgnoreParameter]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [IgnoreParameter]
        public string LastName { get; set; }
    }
}
