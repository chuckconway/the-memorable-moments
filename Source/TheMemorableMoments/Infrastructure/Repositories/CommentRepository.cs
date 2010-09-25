using System;
using System.Collections.Generic;
using System.Data;
using Chucksoft.Core.Extensions;
using Hypersonic;
using TheMemorableMoments.Domain.Model;
using System.Data.Common;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Infrastructure.Repositories
{
	public class CommentRepository : RepositoryBase, ICommentRepository
	{

		/// <summary>
		/// Saves the specified comment.
		/// </summary>
		/// <param name="comment">The comment.</param>
		public void Save(Comment comment)
		{
			if (comment.CommentId > 0)
			{
				Update(comment);
			}
			else
			{
				Insert(comment);
			}
		}

		/// <summary>
		/// Inserts Comment into the Comments Table
		/// </summary>
		/// <param name="comment">A new populated comment.</param>
		/// <returns>Insert Count</returns>
		public int Insert(Comment comment)
		{
			DbParameter outParameter = database.MakeParameter("@Identity", 0, ParameterDirection.Output);

			List<DbParameter> parameters = new List<DbParameter> 
			{
					database.MakeParameter("@Name",comment.Name),
					database.MakeParameter("@Email",comment.Email),
					database.MakeParameter("@SiteUrl",comment.SiteUrl),
					database.MakeParameter("@Ip",comment.Ip),
					database.MakeParameter("@UserAgent",comment.UserAgent),
					database.MakeParameter("@CommentStatus",comment.CommentStatus.ToString()),
					database.MakeParameter("@Text",comment.Text),
					database.MakeParameter("@CommentDate",comment.CommentDate),
					database.MakeParameter("@UserId",comment.UserId),
					database.MakeParameter("@ParentId",comment.ParentId),
					database.MakeParameter("@MediaId",comment.MediaId),
					outParameter
			};

			database.NonQuery("Comment_Insert", parameters);
			return Convert.ToInt32(outParameter.Value);
		}


		/// <summary>
		/// Updates the Comment table by the primary key, if the Comment is dirty then an update will occur
		/// </summary>
		/// <param name="comment">a populated comment</param>
		/// <returns>update count</returns>
		public int Update(Comment comment)
		{
			int updateCount = database.NonQuery("Comment_Update", comment);
			return updateCount;
		}

		/// <summary>
		/// Retrieves the recent 5 comments by user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Comment> RetrieveRecent5CommentsByUserId(int userId)
		{
			List<Comment> comments = database.PopulateCollection("Comment_RetrieveRecent5CommentsByUserId", new { userId }, database.AutoPopulate<Comment>);
			return comments;
		}

		/// <summary>
		/// Retrieves the comments by user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Comment> RetrieveCommentsByUserId(int userId)
		{
			return database.PopulateCollection("Comment_RetrieveCommentsByUserId", new { userId }, database.AutoPopulate<Comment>);
		}

		/// <summary>
		/// Retrieves the comment by status and user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <param name="commentstatus">The commentstatus.</param>
		/// <returns></returns>
		public List<Comment> RetrieveCommentByStatusAndUserId(int userId, CommentStatus commentstatus)
		{
			return database.PopulateCollection("Comment_RetrieveCommentByStatusAndUserId", new{userId, commentstatus = commentstatus.ToString()}, database.AutoPopulate<Comment>);
		}

		//
		/// <summary>
		/// Delete a Comment by the primary key
		/// </summary>
		/// <param name="comment"></param>
		public int Delete(Comment comment)
		{
			return database.NonQuery("Comment_Delete", new{comment.CommentId});
		}

		/// <summary>
		/// Retrieves all.
		/// </summary>
		/// <returns></returns>
		public List<Comment> RetrieveAll()
		{
			return database.PopulateCollection("Comment_RetrieveAll", database.AutoPopulate<Comment>);
		}

		/// <summary>
		/// Retrieves the by primary key.
		/// </summary>
		/// <param name="commentId">The comment id.</param>
		/// <returns></returns>
		public Comment RetrieveByPrimaryKey(int commentId)
		{
			return database.PopulateItem("Comment_RetrieveByPrimaryKey", new { commentId }, database.AutoPopulate<Comment>);
		}

		/// <summary>
		/// Retrieves all by media id.
		/// </summary>
		/// <param name="mediaId">The media id.</param>
		/// <returns></returns>
		public List<Comment> RetrieveAllByMediaId(int mediaId)
		{
			return database.PopulateCollection("Comment_RetrieveAllByMediaId", new{mediaId}, database.AutoPopulate<Comment>);
		}


		/// <summary>
		/// Retrieves the comment status count by user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<StatusCount<CommentStatus>> RetrieveCommentStatusCountByUserId(int userId)
		{
			Func<INullableReader, StatusCount<CommentStatus>> populate = o => new StatusCount<CommentStatus> { Count = o.GetInt32("MediaCount"), Status = o.GetString("CommentStatus").ParseEnum<CommentStatus>() };
			return database.PopulateCollection("Comment_RetrieveCommentStatusCountByUserId", new{userId}, populate);
		}

		/// <summary>
		/// Updates the comment status.
		/// </summary>
		/// <param name="commentStatus">The comment status.</param>
		/// <param name="ids">The ids.</param>
		/// <param name="userId">The user id.</param>
		public void UpdateCommentStatus(CommentStatus commentStatus, int[] ids, int userId)
		{
			foreach (int id in ids)
			{
				UpdateCommentStatus(commentStatus, id, userId);
			}
		}

		/// <summary>
		/// Updates the comment status.
		/// </summary>
		/// <param name="commentStatus">The comment status.</param>
		/// <param name="commentId">The comment id.</param>
		/// <param name="userId">The user id.</param>
		private void UpdateCommentStatus(CommentStatus commentStatus, int commentId, int userId)
		{
			database.NonQuery("Comment_UpdateStatus", new
														   {
															   CommentStatus = commentStatus.ToString(),
															   commentId,
															   userId
														   }
				);
		}

	}
}
