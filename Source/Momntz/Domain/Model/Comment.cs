using System;

namespace Momntz.Domain.Model
{
    public class Comment : IPrimaryKey<int>
    {
        public Comment()
        {
            Status = CommentStatus.Pending;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        /// Gets or sets the user IP.
        /// </summary>
        /// <value>
        /// The user IP.
        /// </value>
        public string UserIp { get; set; }

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

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public CommentStatus Status { get; set; }
       
    }

    public enum CommentStatus
    {
        Pending,
        Spam,
        Approved
    }
}