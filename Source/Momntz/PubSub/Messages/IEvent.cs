using System;
using NServiceBus;

namespace Momntz.PubSub.Messages
{
    public interface IEvent : IMessage
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the created timestamp.
        /// </summary>
        DateTime CreatedTimestamp { get; }

        /// <summary>
        /// Gets the completed timestamp.
        /// </summary>
        DateTime CompletedTimestamp { get; }
    }
}
