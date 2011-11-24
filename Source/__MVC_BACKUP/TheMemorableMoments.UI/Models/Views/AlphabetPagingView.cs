using System.Collections.Generic;
using TheMemorableMoments.Domain.Model.Paging;

namespace TheMemorableMoments.UI.Models.Views
{
    public class AlphabetPagingView : BaseModel
    {
        /// <summary>
        /// Gets or sets the selected letter.
        /// </summary>
        /// <value>The selected letter.</value>
        public char SelectedLetter { get; set; }

        /// <summary>
        /// Gets or sets the alphabet pages.
        /// </summary>
        /// <value>The alphabet pages.</value>
        public List<AlphabetPage> AlphabetPages  { get; set; }
    }
}
