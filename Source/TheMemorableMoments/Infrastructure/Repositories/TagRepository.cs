using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Hypersonic;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.Infrastructure.Repositories
{
	public class TagRepository : RepositoryBase, ITagRepository
	{

		/// <summary>
		/// Inserts Tag into the Tags Table
		/// </summary>
		/// <param name="tag">A new populated tag.</param>
		/// <returns>Insert Count</returns>
		public int Insert(Tag tag)
		{
			DbParameter outParameter = database.MakeParameter("@Identity", 0, ParameterDirection.Output);
		
			List<DbParameter> parameters = new List<DbParameter> 
			{
					database.MakeParameter("@TagName",tag.TagText),
					outParameter
			};

			database.NonQuery("Tag_Insert", parameters); 
			 return Convert.ToInt32(outParameter.Value); 
		}

		/// <summary>
		/// Retrieves the tag by name and user id.
		/// </summary>
		/// <param name="tagName">Name of the tag.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public Tag RetrieveTagByNameAndUserId(string tagName, int userId)
		{
			Tag tag = database.PopulateItem("Tag_RetrieveTagByNameAndUserId", new { tagName, userId }, Populate);
			return tag;
		}

		/// <summary>
		/// Inserts the tag with media id.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <param name="mediaId">The media id.</param>
		public void InsertTagWithMediaId(Tag tag, int mediaId)
		{
			database.NonQuery("Tag_InsertTagWithMediaId", new { TagName = tag.TagText, mediaId });
		}


		/// <summary>
		/// Updates the Tag table by the primary key, if the Tag is dirty then an update will occur
		/// </summary>
		/// <param name="tag">a populated tag</param>
		/// <param name="userId"></param>
		/// <returns>update count</returns>
		public int Update(Tag tag, int userId)
		{
			int updateCount = database.NonQuery("Tag_Update", new { TagId = tag.Id, TagName = tag.TagText, tag.Description, userId });
			return updateCount;
		}


		/// <summary>
		/// Updates the specified post.
		/// </summary>
		/// <param name="tags">The tags.</param>
		/// <param name="mediaId">The media id.</param>
		public void Update(string tags, int mediaId)
		{
			TagCollection internalTags = new TagCollection(tags);
			database.NonQuery("Tag_DeleteTagsByMediaId", new{mediaId});

			foreach (Tag tag in internalTags)
			{
				InsertTagWithMediaId(tag, mediaId);
			}
		}

		/// <summary>
		/// Delete a Tag by the primary key
		/// </summary>
		/// <param name="tag"></param>
		public int Delete(Tag tag)
		{
			return database.NonQuery("Tag_Delete", new{TagId = tag.Id});
		}

		/// <summary>
		/// Retrieves all.
		/// </summary>
		/// <returns></returns>
		public List<Tag> RetrieveAll()
		{
			return database.PopulateCollection("Tag_RetrieveAll", Populate);
		}

		/// <summary>
		/// Retrieves the tags by user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public List<Tag> RetrieveTagsByUserId(int userId)
		{
			return database.PopulateCollection("Tag_RetrieveTagsByUserId", new{userId}, Populate);
		}

		/// <summary>
		/// Retrieves the by primary key.
		/// </summary>
		/// <param name="tagId">The tag id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public Tag RetrieveTagsByUserIdAndTagId(int tagId, int userId)
		{
			return database.PopulateItem("Tag_RetrieveTagsByUserIdAndTagId", new{tagId,userId}, Populate);
		}

		/// <summary>
		/// Searches the specified user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <param name="searchText">The search text.</param>
		/// <returns></returns>
		public List<Tag> Search(int userId, string searchText)
		{
			return database.PopulateCollection("Tag_Search", new{userId, searchText}, Populate);
		}

		/// <summary>
		/// Gets the related tags.
		/// </summary>
		/// <param name="tagId">The tag id.</param>
		/// <returns></returns>
		public List<Tag> GetRelatedTags(int tagId)
		{
			return database.PopulateCollection("Tag_GetRelatedTags", new { tagId }, Populate);
		}

		/// <summary>
		/// Gets the related tags by year.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <param name="year">The year.</param>
		/// <returns></returns>
		public List<Tag> GetRelatedTagsByYear(int userId, int year)
		{
			return database.PopulateCollection("Tag_GetRelatedTagsByYear", new { userId, year }, Populate);
		}

		/// <summary>
		/// Populates the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		internal static Tag Populate(INullableReader reader)
		{
			Tag tag = new Tag(reader.GetString("TagName"))
						  {
							  Id = reader.GetInt32("TagId"),
							  Count = reader.GetInt32("TagCount"),
							  Description = reader.GetNullableString("Description") ?? string.Empty
						  };
			return tag;
		}

	}
}
