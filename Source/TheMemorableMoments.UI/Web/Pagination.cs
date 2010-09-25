using System;
using System.Collections.Generic;
using System.Text;
using TheMemorableMoments.UI.Models;

namespace TheMemorableMoments.UI.Web.Helpers
{
    public class Pagination
    {
        private readonly string _queryFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pagination"/> class.
        /// </summary>
        public Pagination()
        {
            _queryFormat = "?cp={0}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pagination"/> class.
        /// </summary>
        /// <param name="queryFormat">The query format.</param>
        public Pagination(string queryFormat)
        {
            _queryFormat = queryFormat;
        }

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
        /// Renders the pagination.
        /// </summary>
        /// <returns></returns>
        public List<Link> GeneratePagination<T>(List<T> source, int currentIndex, int perPage)
        {
            List<Link> links = new List<Link>();

            //Don't create paging if there are less than one page's worth of items
            if (source.Count > 0 && source.Count > perPage)
            {
                links.Add(new Link(string.Format(_queryFormat, 1), "Return to the First Item", "First"));
                IEnumerable<Link> pages = CreatePages(source, perPage, currentIndex);
                links.AddRange(pages);
            }

            return links;
        }

        /// <summary>
        /// Renders the pagination.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="perPage">The per page.</param>
        /// <returns></returns>
        public string RenderPagination<T>(List<T> source, int currentIndex, int perPage)
        {
            CurrentPage = currentIndex;

            List<Link> links = GeneratePagination(source, currentIndex, perPage);
            string renderedLinks = RenderPagination(links);

            return renderedLinks;
        }

        /// <summary>
        /// Renders the pagination.
        /// </summary>
        /// <param name="links">The links.</param>
        /// <returns></returns>
        public string RenderPagination(List<Link> links)
        {
            StringBuilder builder = new StringBuilder();

            const string link = "<li><a href=\"{0}\" {3} {1} title=\"{4}\">{2}</a></li>";
            builder.AppendLine("<ul class=\"pagination\" >");

            foreach (Link t in links)
            {
                builder.AppendLine(string.Format(link, t.Href,
                                                 (string.IsNullOrEmpty(t.OnClick)
                                                      ? string.Empty
                                                      : string.Format("onclick=\"{0}\"", t.OnClick)), t.Text,
                                                 (string.IsNullOrEmpty(t.Class)
                                                      ? string.Empty
                                                      : string.Format("class=\"{0}\"", t.Class)), t.Title));
            }

            builder.AppendLine("</ul>");
            return builder.ToString();
        }

        /// <summary>
        /// Creates the pages.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="perPage">The per page.</param>
        /// <param name="currentIndex">Index of the current.</param>
        private  IEnumerable<Link> CreatePages<T>(ICollection<T> source, int perPage, int currentIndex)
        {
            //Current item range
            int p = (currentIndex) * perPage;

            //Max items possiable with current page count.
            int maxItemsWithCurrentPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(source.Count) / perPage) * perPage);
            List<Link> pages = GeneratePageLinks(currentIndex, perPage, source);
            pages = PageRange(currentIndex, perPage, pages);

            //Set total Page count
            TotalPages = pages.Count;

            //Next (on top of the stack)
            if (p < maxItemsWithCurrentPageCount) { pages.Insert(0, new Link(string.Format(_queryFormat, currentIndex + 1), "Move to the Next Page", "Next") { Class = string.Empty }); }

            //Previous (on the bottom of the stack)
            if (maxItemsWithCurrentPageCount - perPage >= p && currentIndex > 1) { pages.Add(new Link(string.Format(_queryFormat, currentIndex - 1), "Move to the Next Page", "Previous") { Class = string.Empty }); }

            return pages;
        }

        /// <summary>
        /// Generates the page links.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="perPage">The per page.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private List<Link> GeneratePageLinks<T>(int currentIndex, int perPage, ICollection<T> source)
        {
            List<Link> links = new List<Link>();

            int currentPage = -1;

            for (int index = 0; index < source.Count; index++)
            {
                int page = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(index + 1) / (perPage)));
                if (currentPage < page || currentPage == -1)
                {
                    currentPage = page;
                    links.Add(new Link(string.Format(_queryFormat, page), "Move to the Next Page", page.ToString()) { Class = (currentIndex == currentPage ? "active" : string.Empty) });
                }
            }

            return links;
        }


        /// <summary>
        /// Generates the page links.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="maxPagesShownAtOneTime">The max pages shown at one time.</param>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        private static List<T> PageRange<T>(int currentIndex, int maxPagesShownAtOneTime, List<T> pages)
        {
            if (maxPagesShownAtOneTime < pages.Count)
            {
                maxPagesShownAtOneTime = ((pages.Count - currentIndex) > maxPagesShownAtOneTime
                                              ? maxPagesShownAtOneTime
                                              : pages.Count - currentIndex);
                
                pages = pages.GetRange(currentIndex, maxPagesShownAtOneTime);
            }

            return pages;
        }
    }
}