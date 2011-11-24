using System;

namespace TheMemorableMoments.Domain.Model
{
	public class Configuration 
	{

        /// <summary>
        /// Gets or sets the configuration id.
        /// </summary>
        /// <value>The configuration id.</value>
		public Guid ConfigurationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
		public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the site tag line.
        /// </summary>
        /// <value>The site tag line.</value>
		public string SiteTagLine { get; set; }

        /// <summary>
        /// Gets or sets the membership.
        /// </summary>
        /// <value>The membership.</value>
		public string Membership { get; set; }

        /// <summary>
        /// Gets or sets the height of the thumb nail.
        /// </summary>
        /// <value>The height of the thumb nail.</value>
		public short ThumbNailHeight { get; set; }

        /// <summary>
        /// Gets or sets the width of the thumb nail.
        /// </summary>
        /// <value>The width of the thumb nail.</value>
		public short ThumbNailWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the web.
        /// </summary>
        /// <value>The height of the web.</value>
		public short WebHeight { get; set; }

        /// <summary>
        /// Gets or sets the width of the web.
        /// </summary>
        /// <value>The width of the web.</value>
		public short WebWidth { get; set; }


	}
}
