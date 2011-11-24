using Hypersonic.Attributes;

namespace TheMemorableMoments.Domain.Model.MediaClasses
{
	public class MediaFile 
	{

        /// <summary>
        /// Gets or sets the file id.
        /// </summary>
        /// <value>The file id.</value>
		public int FileId { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
		public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>The file extension.</value>
		public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the name of the original file.
        /// </summary>
        /// <value>The name of the original file.</value>
		public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the media format.
        /// </summary>
        /// <value>The media format.</value>
        public MediaFormat MediaFormat { get; set; }

        /// <summary>
        /// Gets or sets the type of the photo.
        /// </summary>
        /// <value>The type of the photo.</value>
        [DataAlias(Alias = "MediaType")]
        public PhotoType PhotoType { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
        public int MediaId { get; set; }

	}

    public enum MediaFormat
    {
        Video,
        Photo
    }

    public enum PhotoType
    {
        Thumbnail,
        Websize,
        Original
    }
}
