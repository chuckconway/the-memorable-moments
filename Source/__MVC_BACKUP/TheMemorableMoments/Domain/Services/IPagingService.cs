using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Paging;

namespace TheMemorableMoments.Domain.Services
{
    public interface IPagingService
    {
        /// <summary>
        /// Retreives the paging by letter and user id.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        AlphaPaging RetreivePagingByLetterAndUserId(char letter, int userId, int currentPage);
    }
}