using System;

namespace Momntz.PubSub.Messages
{
   public class CommentMessage : IEvent
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

       /// <summary>
       /// Gets or sets the user IP.
       /// </summary>
       /// <value>
       /// The user IP.
       /// </value>
       public string UserIP { get; set; }

       /// <summary>
       /// Gets or sets the user agent.
       /// </summary>
       /// <value>
       /// The user agent.
       /// </value>
       public string UserAgent { get; set; }

       /// <summary>
       /// Gets or sets the body.
       /// </summary>
       /// <value>
       /// The body.
       /// </value>
       public string Body { get; set; }

       /// <summary>
       /// Gets or sets the author.
       /// </summary>
       /// <value>
       /// The author.
       /// </value>
       public string Author { get; set; }

       /// <summary>
       /// Gets or sets the author email.
       /// </summary>
       /// <value>
       /// The author email.
       /// </value>
       public string AuthorEmail { get; set; }

       /// <summary>
       /// Gets or sets the author URL.
       /// </summary>
       /// <value>
       /// The author URL.
       /// </value>
       public string AuthorUrl { get; set; }

    }
}
