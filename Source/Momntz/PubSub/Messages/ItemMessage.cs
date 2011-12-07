using System;

namespace Momntz.PubSub.Messages
{
    public class ItemMessage : IEvent
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; private set; }

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
