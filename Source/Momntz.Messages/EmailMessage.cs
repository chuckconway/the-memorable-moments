using System;

namespace Momntz.Messages
{
    public class EmailMessage : IEvent<Guid>
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the created timestamp.
        /// </summary>
        public DateTime CreatedTimestamp { get; private set; }

        /// <summary>
        /// Gets the completed timestamp.
        /// </summary>
        public DateTime CompletedTimestamp { get; private set; }
    }
}
