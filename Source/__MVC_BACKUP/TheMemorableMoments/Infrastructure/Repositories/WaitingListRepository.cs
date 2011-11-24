namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class WaitingListRepository : RepositoryBase, IWaitingListRepository
    {
        /// <summary>
        /// Inserts the specified email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        public void Insert(string emailAddress)
        {
            database.NonQuery("WaitingList_Insert", new {emailAddress});
        }
    }
}
