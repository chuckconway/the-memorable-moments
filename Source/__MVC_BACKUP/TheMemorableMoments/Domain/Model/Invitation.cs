using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMemorableMoments.Domain.Model
{
    public class Invitation
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string[] Email { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }
    }
}
