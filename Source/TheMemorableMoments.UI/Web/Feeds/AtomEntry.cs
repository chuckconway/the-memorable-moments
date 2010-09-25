using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheMemorableMoments.UI.Web.Feeds
{
    public class AtomEntry
    {
        //    <entry>
        //        <title>Atom-Powered Robots Run Amok</title>
        //        <link href="http://example.org/2003/12/13/atom03" />
        //        <id>urn:uuid:1225c695-cfb8-4ebb-aaaa-80da344efa6a</id>
        //        <updated>2003-12-13T18:30:02Z</updated>
        //        <summary>Some text.</summary>
        //    </entry>

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string Link  { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public string Updated { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }
    }
}