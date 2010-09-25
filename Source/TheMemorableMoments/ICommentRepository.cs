using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments
{
    public interface ICommentRepository
    {

        /// <summary>
        /// Saves the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        void Save(Comment comment);

        /// <summary>
        /// Delete a Comment by the primary key
        /// </summary>
        /// <param name="comment"></param>
        int Delete(Comment comment);

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        List<Comment> RetrieveAll();

        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Comment RetrieveByPrimaryKey(int key);

        /// <summary>
        /// Retrieves all by media id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        List<Comment> RetrieveAllByMediaId(int mediaId);

        /// <summary>
        /// Retrieves the comments by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Comment> RetrieveCommentsByUserId(int userId);

        /// <summary>
        /// Retrieves the recent5 comments by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Comment> RetrieveRecent5CommentsByUserId(int userId);

        /// <summary>
        /// Retrieves the comment status count by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<StatusCount<CommentStatus>> RetrieveCommentStatusCountByUserId(int userId);

        /// <summary>
        /// Retrieves the comment by status and user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        List<Comment> RetrieveCommentByStatusAndUserId(int userId, CommentStatus status);

        /// <summary>
        /// Updates the comment status.
        /// </summary>
        /// <param name="commentStatus">The comment status.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="userId">The user id.</param>
        void UpdateCommentStatus(CommentStatus commentStatus, int[] ids, int userId);
    }
}