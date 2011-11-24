using System;

namespace TheMemorableMoments.Domain.Model
{
	public class Comment 
	{

        /// <summary>
        /// Gets or sets the comment id.
        /// </summary>
        /// <value>The comment id.</value>
		public int CommentId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
		public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
		public string Email { get; set; }

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>The site URL.</value>
		public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>The ip.</value>
		public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
		public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the comment status.
        /// </summary>
        /// <value>The comment status.</value>
        public CommentStatus CommentStatus { get; set; }

        /// <summary>
        /// Gets or sets the markdown text.
        /// </summary>
        /// <value>The markdown text.</value>
		public string Text { get; set; }

        /// <summary>
        /// Gets or sets the comment date.
        /// </summary>
        /// <value>The comment date.</value>
		public DateTime CommentDate { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
		public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
		public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
		public int MediaId { get; set; }


	}

    /// <summary>
    /// 
    /// </summary>
    public enum CommentStatus
    {
        /// <summary>
        /// 
        /// </summary>
        Approved,
        /// <summary>
        /// 
        /// </summary>
        Pending,
        /// <summary>
        /// 
        /// </summary>
        Deleted,
        /// <summary>
        /// 
        /// </summary>
        Spam
    }
}
