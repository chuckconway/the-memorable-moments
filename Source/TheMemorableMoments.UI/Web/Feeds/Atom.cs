using System;
using System.Collections.Generic;
using System.Text;

namespace TheMemorableMoments.UI.Web.Feeds
{
    public class Atom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom"/> class.
        /// </summary>
        public Atom()
        {
            Entries = new List<AtomEntry>();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the sub title.
        /// </summary>
        /// <value>The sub title.</value>
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the link self.
        /// </summary>
        /// <value>The link self.</value>
        public string LinkSelf { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>The updated date.</value>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        /// <value>The name of the author.</value>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the author email.
        /// </summary>
        /// <value>The author email.</value>
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public List<AtomEntry> Entries { get; set; }


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            const string atomFormat = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <feed xmlns=""http://www.w3.org/2005/Atom"">
                <title>{0}</title>
                <subtitle>{1}</subtitle>
                <link href=""{2}"" rel=""self"" />
                <link href=""{3}"" />
                <id>urn:uuid:{4}</id>
                <updated>{5}</updated>
                <author>
                    <name>{6}</name>
                    <email>{7}</email>
                </author>
                {8}
            </feed>";

            return string.Format(atomFormat, Title, SubTitle, LinkSelf, Link, Id, UpdatedDate.ToString("u"), AuthorName, AuthorName, RenderEntries(Entries));
        }

        /// <summary>
        /// Renders the entries.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <returns></returns>
        private static string RenderEntries(IEnumerable<AtomEntry> entries)
        {
            StringBuilder builder = new StringBuilder();

            const string entryFormat = @"
                <entry>
                    <title>{0}</title>
                    <link href=""{1}"" />
                    <id>urn:uuid:{2}</id>
                    <updated>{3}</updated>
                    <summary type=""xhtml"">{4}</summary>
                    <content type=""html"">{5}</content>
                </entry>";

            foreach (AtomEntry entry in entries)
            {
                builder.Append(string.Format(entryFormat, entry.Title, entry.Link, entry.Id, entry.Updated, entry.Summary, entry.Summary));
            }

            return builder.ToString();
        }

        //<?xml version="1.0" encoding="utf-8"?>
         
        //<feed xmlns="http://www.w3.org/2005/Atom">
         
        //    <title>Example Feed</title>
        //    <subtitle>A subtitle.</subtitle>
        //    <link href="http://example.org/feed/" rel="self" />
        //    <link href="http://example.org/" />
        //    <id>urn:uuid:60a76c80-d399-11d9-b91C-0003939e0af6</id>
        //    <updated>2003-12-13T18:30:02Z</updated>
        //    <author>
        //        <name>John Doe</name>
        //        <email>johndoe@example.com</email>
        //    </author>
         
        //    <entry>
        //        <title>Atom-Powered Robots Run Amok</title>
        //        <link href="http://example.org/2003/12/13/atom03" />
        //        <id>urn:uuid:1225c695-cfb8-4ebb-aaaa-80da344efa6a</id>
        //        <updated>2003-12-13T18:30:02Z</updated>
        //        <summary>Some text.</summary>
        //    </entry>
         
        //</feed>

    }
}