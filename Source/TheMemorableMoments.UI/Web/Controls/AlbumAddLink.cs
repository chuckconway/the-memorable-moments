namespace TheMemorableMoments.UI.Web.Controls
{
    public class AlbumAddLink : IAlbumTabText
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>The href.</value>
        public string Href { get; set; }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            return string.Format("<a class=\"youarehere\" href=\"{0}\">{1}</a>",Href, Text);
        }
    }
}