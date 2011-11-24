using System.Collections.Generic;
using System.Data.Common;
using Hypersonic;
using TheMemorableMoments.Domain.Model.Paging;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.Infrastructure.Repositories
{
   public class PagingRepository : RepositoryBase, IPagingRepository
   {
       /// <summary>
       /// Retrieves the paging by user id.
       /// </summary>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public List<AlphabetPage> RetrievePagingByUserId(int userId)
       {
           List<DbParameter> parameters = new List<DbParameter> { database.MakeParameter("@UserId", userId) };
           return database.PopulateCollection("Paging_RetrievePagingByUserId", parameters, Populate);
       }

       /// <summary>
       /// Retrieves the paging by user id and letter.
       /// </summary>
       /// <param name="letter">The letter.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="currentPage">The current page.</param>
       /// <param name="pageSize">Size of the page.</param>
       /// <returns></returns>
       public List<Tag> RetrievePagingByUserIdAndLetter(char letter, int userId, int currentPage, int pageSize)
       {
           return database.PopulateCollection("Paging_RetrievePagingByUserIdAndLetter", new
                   {
                       letter, 
                       userId,
                       currentPage, 
                       pageSize
                   },
               TagRepository.Populate);
       }

       /// <summary>
       /// Retrieves the paging count by user id and letter.
       /// </summary>
       /// <param name="letter">The letter.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="?">The ?.</param>
       /// <returns></returns>
       public int RetrievePagingCountByUserIdAndLetter(char letter, int userId)
       {
           return (int)database.Scalar("Paging_RetrievePagingCountByUserIdAndLetter", new{letter, userId});
       }

       /// <summary>
       /// Populates the specified reader.
       /// </summary>
       /// <param name="reader">The reader.</param>
       /// <returns></returns>
       private static AlphabetPage Populate(INullableReader reader)
       {
           AlphabetPage alphabetPage = new AlphabetPage
                                           {
                                               Letter = reader.GetString("PageIndex"),
                                               MediaCount = reader.GetInt32("MediaCountInLetter")
                                           };
           return alphabetPage;
       }
    }
}
