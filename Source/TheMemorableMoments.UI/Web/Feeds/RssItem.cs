using System;

namespace TheMemorableMoments.UI.Web.Feeds
{
    public class RssItem
    {
        //   <item>
        //      <title>Dealing with Header and Footer Weirdness</title>
        //      <description>Avoid aggravation by deactivating the Same as Previous button.</description>
        //      <link>http://www.LogicalTips.com/LPMArticle.asp?ID=356</link>
        //      <pubDate>4/17/2004 11:02:18 AM</pubDate>
        //      <guid isPermaLink="true">http://www.LogicalTips.com/LPMArticle.asp?ID=356</guid>
        //   </item>

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        /// <value>The publish date.</value>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        /// <value>The GUID.</value>
        public string Guid { get; set; }
    }
}