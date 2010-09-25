using System;
using System.Text;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Paging;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Web
{
    public class PagingHelper
    {
        /// <summary>
        /// Renders the alphabet paging.
        /// </summary>
        /// <param name="alphabetPages">The alphabet pages.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static string RenderAlphabetPaging(AlphabetPagingView alphabetPages, User user)
        {
            IUserUrlService urlService = UserUrlService.GetInstance(user);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<ul class=\"alphabet\">");
            const string haveTagsFormat = "<li {1} ><a  href=\"{2}\" >{0}</a></li>";
            const string doesntHaveTagsFormat = "<li>{0}</li>";

            foreach (AlphabetPage alphabetPage in alphabetPages.AlphabetPages)
            {
                string isSelected = (alphabetPage.Letter.Equals(alphabetPages.SelectedLetter.ToString(), StringComparison.InvariantCultureIgnoreCase)
                                         ? "class=\"selected\""
                                         : string.Empty);

                builder.AppendLine(alphabetPage.MediaCount > 0
                    ? string.Format(haveTagsFormat, alphabetPage.Letter, isSelected, urlService.CreateUrl( "tags/show/" + alphabetPage.Letter)) 
                    : string.Format(doesntHaveTagsFormat, alphabetPage.Letter));
            }

            builder.AppendLine("</ul>");
            return builder.ToString();
        }

        /// <summary>
        /// Renders the numeric paging.
        /// </summary>
        /// <param name="numericPaging">The numeric paging.</param>
        /// <param name="user">The user.</param>
        /// <param name="letter">The letter.</param>
        /// <returns></returns>
        public static string RenderNumericPaging(NumericPaging numericPaging, User user, char letter)
        {
            string paging = string.Empty;

            if (!(numericPaging.PageSize > numericPaging.TotalCount))
            {
                IUserUrlService urlService = UserUrlService.GetInstance(user);
                int pages = Convert.ToInt32(Math.Ceiling(numericPaging.TotalCount / Convert.ToDecimal(numericPaging.PageSize)));

                const string linkFormat = "<li><a href=\"{0}\" title=\"View page {1}\" {3} >{2}</a></li>";
                StringBuilder builder = new StringBuilder();

                for (int index = 1; index <= pages; index++)
                {
                    string selectedText = (numericPaging.CurrentPage == index ? "class=\"active\"" : string.Empty );
                    builder.AppendLine(string.Format(linkFormat, urlService.CreateUrl("tags/show/" + letter + "?page=" + index), index, index, selectedText));
                }

                paging = builder.ToString();
            }

            return paging;
        }
    }
}
