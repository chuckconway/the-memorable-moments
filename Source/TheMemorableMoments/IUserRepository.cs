using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments
{
    public interface IUserRepository
    {
        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        int Save(User user);

        /// <summary>
        /// Delete a User by the primary key
        /// </summary>
        /// <param name="user"></param>
        int Delete(User user);

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        List<User> RetrieveAll();

        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        User RetrieveByPrimaryKey(int key);

        /// <summary>
        /// Retrieves the user by login credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        User RetrieveUserByLoginCredentials(string username, string password);

        /// <summary>
        /// Retrieves the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        User RetrieveUserByUsername(string username);

        /// <summary>
        /// Retrieves the random photo by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        Media RetrieveRandomPhotoByUserId(int userId);

        /// <summary>
        /// Checks the uniqueness.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        int CheckAvailability(string username);

    }
}