using Hypersonic.Attributes;

namespace TheMemorableMoments.Domain.Model
{
    public class RecentActivity
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        [DataAlias(Alias = "ActivityCount")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ActivityType ActivityType { get; set; }

    }

    public enum ActivityType
    {
        PhotosAdded,
        FriendsAdded,
        NewAlbums,
        NewComments,
        AddedPhotosToAlbums,
        TagsAddedToPhotos
    }
}
