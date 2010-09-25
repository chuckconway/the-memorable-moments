using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Paging;
using TheMemorableMoments.UI.Web;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views
{
    public class MediaGroupedByTagsView : BaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the media by tags.
        /// </summary>
        /// <value>The media by tags.</value>
        public List<MediaGroupedByTag> MediaByTags { get; set; }

        /// <summary>
        /// Gets or sets the alphabet paging.
        /// </summary>
        /// <value>The alphabet paging.</value>
        public AlphabetPagingView AlphabetPagingView { get; set; }

        /// <summary>
        /// Gets or sets the numeric paging.
        /// </summary>
        /// <value>The numeric paging.</value>
        public NumericPaging NumericPaging { get; set; }

        /// <summary>
        /// Gets or sets the persistent collection.
        /// </summary>
        /// <value>The persistent collection.</value>
        public IPersistentCollectionService PersistentCollection { private get; set; }

        /// <summary>
        /// Sets the dynamic collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public string SetPersistentCollection(string key, List<Media> media)
        {
            PersistentCollection.SetBackUrl(string.Format("{0}/tags", Authorization.Owner.Username), Cookie);
            string setKey = PersistentCollection.Set(Authorization.Owner.Username + "_" + key + "_recent", media, Persistence.Permanent);

            return setKey;
        }

        /// <summary>
        /// Gets or sets the cookie.
        /// </summary>
        /// <value>The cookie.</value>
        public SiteCookie Cookie { private get; set; }
    }
}
