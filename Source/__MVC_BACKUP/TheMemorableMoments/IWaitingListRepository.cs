namespace TheMemorableMoments.Infrastructure.Repositories
{
    public interface IWaitingListRepository
    {
        /// <summary>
        /// Inserts the specified email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        void Insert(string emailAddress);
    }
}