using System.Collections.Generic;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models
{
    public interface IPaginationService<T>
    {
        /// <summary>
        /// Gets or sets the rendered pagination.
        /// </summary>
        /// <value>The rendered pagination.</value>
        string RenderedPagination { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the page set.
        /// </summary>
        /// <value>The page set.</value>
        PagedList<T> PageSet { get; set; }

        /// <summary>
        /// Generates the paging.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="perPage">The per page.</param>
        /// <param name="queryFormat">The query format.</param>
        void GeneratePaging(List<T> media, int currentPage, int perPage, string queryFormat);
    }
}