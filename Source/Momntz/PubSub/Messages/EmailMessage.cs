using System;
using System.Collections.Generic;

namespace Momntz.PubSub.Messages
{
    public class EmailMessage : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="from">From.</param>
        /// <param name="template">The template.</param>
        /// <param name="tokens">The tokens.</param>
        public EmailMessage(int userId, string from, string template, IDictionary<string, string> tokens)
        {
            UserId = userId;
            From = from;
            Template = template;
            Tokens = tokens;
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public int UserId { get; private set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public string From { get; private set; }

        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        /// <value>
        /// The tokens.
        /// </value>
        public IDictionary<string, string> Tokens { get; set; } 

        /// <summary>
        /// Gets the created timestamp.
        /// </summary>
        public DateTime CreatedTimestamp { get; private set; }

        /// <summary>
        /// Gets the completed timestamp.
        /// </summary>
        public DateTime CompletedTimestamp { get; set; }

        /// <summary>
        /// Gets the template.
        /// </summary>
        public string Template { get; private set; }
    }
}
