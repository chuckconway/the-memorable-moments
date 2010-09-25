using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Paging;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments
{
    public interface IPagingRepository
    {
        /// <summary>
        /// Retrieves the paging by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<AlphabetPage> RetrievePagingByUserId(int userId);

        /// <summary>
        /// Retrieves the paging by user id and letter.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        List<Tag> RetrievePagingByUserIdAndLetter(char letter, int userId, int currentPage, int pageSize);

        /// <summary>
        /// Retrieves the paging count by user id and letter.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        int RetrievePagingCountByUserIdAndLetter(char letter, int userId);
    }
}