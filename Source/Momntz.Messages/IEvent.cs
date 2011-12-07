using System;
using NServiceBus;

namespace Momntz.Messages
{
    public interface IEvent<I> : IMessage
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        I Id { get; }

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
