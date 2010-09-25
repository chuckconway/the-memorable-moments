using System.Collections.Generic;
using TheMemorableMoments.UI.Web.Helpers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models
{
    public class PaginationService<T> : IPaginationService<T>
    {
        /// <summary>
        /// Gets or sets the rendered pagination.
        /// </summary>
        /// <value>The rendered pagination.</value>
        public string RenderedPagination { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the page set.
        /// </summary>
        /// <value>The page set.</value>
        public PagedList<T> PageSet { get; set; }

        /// <summary>
        /// Generates the paging.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="perPage">The per page.</param>
        /// <param name="queryFormat">The query format.</param>
        public void GeneratePaging(List<T> media, int currentPage, int perPage, string queryFormat)
        {
            int index = (currentPage < 0 ? 1 : currentPage);
            PageSet = new PagedList<T>(media, index, perPage);
            Pagination pagination = new Pagination(queryFormat);
            RenderedPagination = pagination.RenderPagination(media, index, perPage);
            CurrentPage = pagination.CurrentPage;
            TotalPages = pagination.TotalPages;
        }


    }
}