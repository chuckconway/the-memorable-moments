using System;
using System.Linq;

namespace Momntz.Domain.Model
{
    public class Momento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Momento"/> class.
        /// </summary>
        public Momento()
        {
            Items = new IdentityCollection<Item>();
            Comments = new IdentityCollection<Comment>();
            Tags = new IdentityCollection<Tag>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int? Year { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public int? Month { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        public int? Day { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public IdentityCollection<Tag> Tags { get; set; } 

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IdentityCollection<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public IdentityCollection<Comment> Comments { get; set; } 

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        /// <value>
        /// The visibility.
        /// </value>
        public Visibility Visibility { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Marks the comment as spam.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        public void MarkCommentAsSpam(int commentId)
        {
            const CommentStatus status = CommentStatus.Spam;
            SetCommentStatus(commentId, status);
        }

        /// <summary>
        /// Marks the comment as spam.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        public void MarkCommentAsApproved(int commentId)
        {
            const CommentStatus status = CommentStatus.Spam;
            SetCommentStatus(commentId, status);
        }

        /// <summary>
        /// Sets the comment status.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="status">The status.</param>
        private void SetCommentStatus(int commentId, CommentStatus status)
        {
            var comment = Comments.Where(c => c.Id == commentId).SingleOrDefault();

            if (comment != null)
            {
                comment.Status = status;
            }
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void AddComment(Comment comment)
        {
            comment.Timestamp = DateTime.UtcNow;
            Comments.Add(comment);
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        public void DeleteComment(int commentId)
        {
            Comment comment = Comments.Where(c => c.Id == commentId).SingleOrDefault();

            if(comment != null)
            {
                Comments.Remove(comment);
            }
        }
    }

    public enum Visibility
    {
        Public,
        Network,
        Private
    }
}