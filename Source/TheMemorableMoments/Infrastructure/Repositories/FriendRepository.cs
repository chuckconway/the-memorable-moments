using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.Infrastructure.Repositories
{
   public class FriendRepository : RepositoryBase, IFriendRepository
   {
       private readonly IUserRepository _userRepository;

       /// <summary>
       /// Initializes a new instance of the <see cref="FriendRepository"/> class.
       /// </summary>
       /// <param name="userRepository">The user repository.</param>
       public FriendRepository(IUserRepository userRepository)
       {
           _userRepository = userRepository;
       }
       

        /// <summary>
        /// Inserts the specified friend.
        /// </summary>
        /// <param name="friendId">The friend id.</param>
        /// <param name="userId">The user id.</param>
       public void Insert(int friendId, int userId)
        {
            database.NonQuery("Friend_Insert", new{friendId, userId});
        }


       /// <summary>
       /// Retrieves the friend by id.
       /// </summary>
       /// <param name="friendId">The friend id.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public Friend RetrieveFriendById(int friendId, int userId)
       {
           return database.PopulateItem("Friend_RetrieveFriendById", new { friendId, userId }, database.AutoPopulate<Friend>);
       }

       /// <summary>
       /// Removes the specified friend id.
       /// </summary>
       /// <param name="friendId">The friend id.</param>
       /// <param name="userId">The user id.</param>
       public void Remove(int friendId, int userId)
       {
           database.NonQuery("Friend_Delete", new { friendId, userId });
       }

       /// <summary>
       /// Retrieves the friends by user id.
       /// </summary>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public List<Friend> RetrieveFriendsByUserId(int userId)
       {
           List<Friend> friends = database.PopulateCollection("Friend_RetrievesFriendByUserId", new { userId }, database.AutoPopulate<Friend>);
           friends.ForEach(o => o.Media = _userRepository.RetrieveRandomPhotoByUserId(o.FriendId));

           return friends;
       }

       /// <summary>
       /// Finds the friends.
       /// </summary>
       /// <param name="search">The search.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public List<Friend> FindFriends(string search, int userId)
       {
           List<Friend> friends = database.PopulateCollection("Friend_SearchByText", new { search, userId }, database.AutoPopulate<Friend>);
           friends.ForEach(o => o.Media = _userRepository.RetrieveRandomPhotoByUserId(o.FriendId));


           return friends;
       }

    }
}
