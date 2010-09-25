using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheMemorableMoments.UI.Web.Controls
{
    public class AlbumEditText : IAlbumTabText
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            return string.Format("<span class='youarehere' >{0}</span>", Text);
        }

    }
}