using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments
{
    public interface IFriendRepository
    {
        /// <summary>
        /// Inserts the specified friend.
        /// </summary>
        /// <param name="friendId">The friend id.</param>
        /// <param name="userId">The user id.</param>
        void Insert(int friendId, int userId);

        /// <summary>
        /// Removes the specified friend id.
        /// </summary>
        /// <param name="friendId">The friend id.</param>
        /// <param name="userId">The user id.</param>
        void Remove(int friendId, int userId);

        /// <summary>
        /// Retrieves the friends by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Friend> RetrieveFriendsByUserId(int userId);

        /// <summary>
        /// Retrieves the friend by id.
        /// </summary>
        /// <param name="friendId">The friend id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        Friend RetrieveFriendById(int friendId, int userId);

        /// <summary>
        /// Finds the friends.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Friend> FindFriends(string text, int userId);
    }
}