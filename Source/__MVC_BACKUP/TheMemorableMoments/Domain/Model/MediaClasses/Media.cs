using System;
using System.Collections.Generic;
using System.Linq;
using Hypersonic.Attributes;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.Tags;

namespace TheMemorableMoments.Domain.Model.MediaClasses
{
    public class Media : IMediaFiles
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Media"/> class.
		/// </summary>
		public Media()
		{
			Title = string.Empty;
			Description = string.Empty;
			Tags = string.Empty;
		    Location = new Location();
		}

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        /// <value>The media id.</value>
        public int MediaId { get; set; }

		/// <summary>
		/// Gets or sets the create date.
		/// </summary>
		/// <value>The create date.</value>
        [IgnoreParameter]
		public DateTime CreateDate { get; set; }

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
		/// Gets or sets the year.
		/// </summary>
		/// <value>The year.</value>
		[DataAlias(Alias="MediaYear")]
		public int? Year { get; set; }

		/// <summary>
		/// Gets or sets the month.
		/// </summary>
		/// <value>The month.</value>
		[DataAlias(Alias = "MediaMonth")]
		public int? Month  { get; set; }

		/// <summary>
		/// Gets or sets the day.
		/// </summary>
		/// <value>The day.</value>
		[DataAlias(Alias = "MediaDay")]
		public int? Day { get; set; }

		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		/// <value>The user id.</value>
		public Owner Owner { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public MediaStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public Location Location { get; set; }

		/// <summary>
		/// Gets or sets the media files.
		/// </summary>
		/// <value>The media files.</value>
		[IgnoreParameter]
		public List<MediaFile> MediaFiles { get; set; }

        /// <summary>
        /// Gets or sets the belongs to albums.
        /// </summary>
        /// <value>The belongs to albums.</value>
        [IgnoreParameter]
        public List<BelongsToAlbum> BelongsToAlbums { get; set; }

		/// <summary>
		/// Gets or sets the comment count.
		/// </summary>
		/// <value>The comment count.</value>
        [IgnoreParameter]
		public int CommentCount { get; set; }

		/// <summary>
		/// Gets or sets the metadata.
		/// </summary>
		/// <value>The metadata.</value>
		[IgnoreParameter]
		public List<Exif> Metadata { get; set; }

		/// <summary>
		/// Gets the type of the image by photo.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public MediaFile GetImageByPhotoType(PhotoType type)
		{
			MediaFile mediaFile = new MediaFile();

			foreach (MediaFile file in MediaFiles.Where(file => file.PhotoType == type))
			{
				mediaFile = file;
				break;
			}

			return mediaFile;
		}

		/// <summary>
		/// Adds the tags.
		/// </summary>
		/// <param name="newTags">The new tags.</param>
		public void AddTags(IEnumerable<Tag> newTags)
		{
			_tagCollection.Tags.AddRange(newTags);
		}

		/// <summary>
		/// Gets or sets the tags.
		/// </summary>
		/// <value>The tags.</value>
		private TagCollection _tagCollection;
		public string Tags
		{
			get
			{
				return _tagCollection.ToString();
			}
			set
			{
				_tagCollection = new TagCollection(value);
			}
		}
	}

	public enum MediaStatus
	{
		Public,
		InNetwork,
		Private
	}

}
