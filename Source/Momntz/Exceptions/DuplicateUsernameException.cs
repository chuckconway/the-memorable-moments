using System;

namespace Momntz.Exceptions
{
    public class DuplicateUsernameException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicationUsernameException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DuplicateUsernameException(string message) : base(message) { }
    }
}
