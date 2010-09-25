using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace TheMemorableMoments.UI.Web.Feeds
{
    public class Rss
    {
        //<rss version="2.0">
        //<channel>
        //   <title>Logical Tips</title>
        //   <link>http://www.LogicalTips.com</link>
        //   <description>Computing tips and musings</description>
        //   <language>en-us</language>
        //   <item>
        //      <title>Just Turn It Off</title>
        //      <description>Why meeting with people who refuse to turn off their cell phones is 
        //      pointless.</description>
        //      <link>http://www.LogicalTips.com/LPMArticle.asp?ID=355</link>
        //      <pubDate>4/17/2004 11:00:13 AM</pubDate>
        //      <guid isPermaLink="true">http://www.LogicalTips.com/LPMArticle.asp?ID=355</guid>
        //   </item>
        //   <item>
        //      <title>Dealing with Header and Footer Weirdness</title>
        //      <description>Avoid aggravation by deactivating the Same as Previous button.</description>
        //      <link>http://www.LogicalTips.com/LPMArticle.asp?ID=356</link>
        //      <pubDate>4/17/2004 11:02:18 AM</pubDate>
        //      <guid isPermaLink="true">http://www.LogicalTips.com/LPMArticle.asp?ID=356</guid>
        //   </item>
        //</channel>
        //</rss> 

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<RssItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        /// <value>The publish date.</value>
        public DateTime PublishDate { get; set; }

        

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            const string rssFormat = @"<?xml version=""1.0""?>
            <rss version=""2.0"">
            <channel>
              <title>{0}</title>
              <link>{1}</link>
              <description>{2}</description>
              <language>{3}</language>
              <pubDate>{4}</pubDate>
              <lastBuildDate>{4}</lastBuildDate>
              <docs>{1}</docs>
              <generator>Onyx Weblog Software 0.10</generator>
              <managingEditor>editor@example.com</managingEditor>
              <webMaster>webmaster@chucksoft.com</webMaster>
                 {5}
           </channel>
           /</rss> ";

            string feed = string.Format(rssFormat, HttpUtility.HtmlEncode(Title), Link, HttpUtility.HtmlEncode(Description), Language, PublishDate.ToString("R"),  RenderItems(Items));
            return feed;
        }

        /// <summary>
        /// Renders the Rss items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        private static string RenderItems(IEnumerable<RssItem> items)
        {
            StringBuilder builder = new StringBuilder();
            const string itemFormat = @"
                <item>
                  <title>{0}</title>
                  <description>{1}</description>
                  <link>{2}</link>
                  <pubDate>{3}</pubDate>
                  <guid isPermaLink=""true"">{4}</guid>
               </item>";

            foreach (RssItem item in items)
            {
                builder.AppendLine(string.Format(itemFormat, HttpUtility.HtmlEncode(item.Title),  HttpUtility.HtmlEncode(item.Description), item.Link, item.PublishDate.ToString("R"), item.Guid));
            }

            return builder.ToString();
        }

    }
}