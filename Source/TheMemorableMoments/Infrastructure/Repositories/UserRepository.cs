using System;
using System.Collections.Generic;
using System.Data;
using Chucksoft.Core.Extensions;
using Hypersonic;
using TheMemorableMoments.Domain.Model;
using System.Data.Common;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Infrastructure.Repositories.Services;

namespace TheMemorableMoments.Infrastructure.Repositories
{
	public class UserRepository : RepositoryBase, IUserRepository
	{
		private readonly IMediaFileHydrationService _hydrationService;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		/// <param name="hydrationService">The hydration service.</param>
		public UserRepository(IMediaFileHydrationService hydrationService)
		{
			_hydrationService = hydrationService;
		}


		/// <summary>
		/// Saves the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public int Save(User user)
		{
			int result = (user.Id > 0 ? Update(user) : Insert(user));
			return result;
		}

		/// <summary>
		/// Inserts User into the Users Table
		/// </summary>
		/// <param name="user">A new populated user.</param>
		/// <returns>Insert Count</returns>
		private int Insert(User user)
		{
			DbParameter outParameter = database.MakeParameter("@Identity", 0, ParameterDirection.Output);

			List<DbParameter> parameters = new List<DbParameter> 
			{
					database.MakeParameter("@FirstName",user.FirstName),
					database.MakeParameter("@LastName",user.LastName),
					database.MakeParameter("@Password",user.Password),
					database.MakeParameter("@Email",user.Email),
					database.MakeParameter("@DisplayName",user.DisplayName),
					database.MakeParameter("@Deleted",user.Deleted),
					database.MakeParameter("@Username",user.Username),
					database.MakeParameter("@AccountStatus",AccountStatus.Public),
					outParameter
			};

			database.NonQuery("User_Insert", parameters);
			int userId = Convert.ToInt32(outParameter.Value);

			return userId;
		}


		/// <summary>
		/// Updates the User table by the primary key, if the User is dirty then an update will occur
		/// </summary>
		/// <param name="user">a populated user</param>
		/// <returns>update count</returns>
		private int Update(User user)
		{
			return database.NonQuery("User_Update", user);
		}

		/// <summary>
		/// Delete a User by the primary key
		/// </summary>
		/// <param name="user"></param>
		public int Delete(User user)
		{
			return database.NonQuery("User_Delete", new { UserId = user.Id });
		}

		/// <summary>
		/// Retrieves all.
		/// </summary>
		/// <returns></returns>
		public List<User> RetrieveAll()
		{
			return database.PopulateCollection("User_SelectAll", Populate);
		}

		/// <summary>
		/// Retrieves the by primary key.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public User RetrieveByPrimaryKey(int userId)
		{
			User user = database.PopulateItem("User_SelectByPrimaryKey", new { userId }, database.AutoPopulate<User>);
			return user;
		}

		/// <summary>
		/// Retrieves the user by login credentials.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public User RetrieveUserByLoginCredentials(string username, string password)
		{
			User user = database.PopulateItem("User_RetrieveUserByLoginCredentials", new { username, password }, database.AutoPopulate<User>);
			return user;
		}

		/// <summary>
		/// Retrieves the user by username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns></returns>
		public User RetrieveUserByUsername(string username)
		{
			return database.PopulateItem("User_RetrieveByUsername", new { username }, database.AutoPopulate<User>);
		}

		/// <summary>
		/// Retrieves the random photo by user id.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		public Media RetrieveRandomPhotoByUserId(int userId)
		{
			List<Media> media = database.PopulateCollection("User_RetrieveRandomPhotoByUserId", new { userId }, database.AutoPopulate<Media>);
            _hydrationService.Populate(media);
			Media singleMedia = (media.Count > 0 ? media[0] : null);

			return singleMedia;
		}

		/// <summary>
		/// Checks the uniqueness.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns></returns>
		public int CheckAvailability(string username)
		{
			object val = database.Scalar("User_CheckAvailability", new { username });
			return Convert.ToInt32(val);
		}

		/// <summary>
		/// Populates the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		internal static User Populate(INullableReader reader)
		{
			User user = new User
					  {
						  Id = reader.GetInt32("UserId"),
						  FirstName = reader.GetString("FirstName"),
						  LastName = reader.GetString("LastName"),
						  Password = reader.GetString("Password"),
						  Email = reader.GetString("Email"),
						  DisplayName = reader.GetString("DisplayName"),
						  Deleted = reader.GetBoolean("Deleted"),
						  Username = reader.GetString("Username"),
						  AccountStatus = reader.GetString("AccountStatus").ParseEnum<AccountStatus>(),
						  Settings = new UserSettings
										 {
											 EnableReceivingOfEmails = reader.GetBoolean("EnableReceivingOfEmails"),
											 WebViewMaxHeight = reader.GetInt16("WebViewMaxHeight"),
											 WebViewMaxWidth = reader.GetInt16("WebViewMaxWidth")
										 }

					  };

			return user;
		}

	}
}
