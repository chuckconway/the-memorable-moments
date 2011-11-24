using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Paging;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.Infrastructure.Repositories;

namespace TheMemorableMoments.Domain.Services
{
    public class PagingService : IPagingService
    {
        private readonly IPagingRepository _pagingRepository;
        private readonly IMediaRepository _mediaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingService"/> class.
        /// </summary>
        /// <param name="pagingRepository">The paging repository.</param>
        /// <param name="mediaRepository">The media repository.</param>
        public PagingService(IPagingRepository pagingRepository, IMediaRepository mediaRepository)
        {
            _pagingRepository = pagingRepository;
            _mediaRepository = mediaRepository;
        }


        /// <summary>
        /// Retreives the paging by letter and user id.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        public AlphaPaging RetreivePagingByLetterAndUserId(char letter, int userId, int currentPage)
        {
            List<Tag> tags = _pagingRepository.RetrievePagingByUserIdAndLetter(letter, userId, currentPage, 5);

            AlphaPaging alphaPaging = new AlphaPaging
                                          {
                                              MediaGroupedByTags = _mediaRepository.RetrieveMediaGroupedByTagsAndUserId(tags, userId),
                                              TagCount = _pagingRepository.RetrievePagingCountByUserIdAndLetter(letter, userId)
                                          };

            return alphaPaging;
        }
    }
}
