namespace TheMemorableMoments.UI.Models
{
    public class Link
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        public Link(string href, string title, string text)
        {
            Href = href;
            Title = title;
            Text = text;
        }

        /// <summary>
        /// Gets or sets the onclick.
        /// </summary>
        /// <value>The on click.</value>
        public string OnClick { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>The href.</value>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>The class.</value>
        public string Class { get; set; }

    }
}