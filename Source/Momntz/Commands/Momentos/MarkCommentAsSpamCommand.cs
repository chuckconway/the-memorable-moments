namespace Momntz.Commands.Momentos
{
    public class MarkCommentAsSpamCommand
    {
        /// <summary>
        /// Gets the momento id.
        /// </summary>
        public int MomentoId { get; private set; }

        /// <summary>
        /// Gets or sets the comment id.
        /// </summary>
        /// <value>
        /// The comment id.
        /// </value>
        public int CommentId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkCommentAsSpamCommand"/> class.
        /// </summary>
        /// <param name="momentoId">The momento id.</param>
        /// <param name="commentId">The comment id.</param>
        public MarkCommentAsSpamCommand(int momentoId, int commentId)
        {
            MomentoId = momentoId;
            CommentId = commentId;
        }
    }
}
