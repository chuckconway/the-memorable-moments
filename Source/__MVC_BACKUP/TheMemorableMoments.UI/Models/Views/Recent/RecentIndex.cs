using System.Collections.Generic;
using System.Linq;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Recent;
using TheMemorableMoments.UI.Web;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Models.Views.Recent
{
    public class RecentIndex : BaseModel
    {
       static readonly List<PhotoAgeFriendlyName>  _photoAgeFriendlyNames = new List<PhotoAgeFriendlyName>
                                                                   {
                                                                       new PhotoAgeFriendlyName {Name = "1 day ago", PhotoAge = PhotoAge.DayOne},
                                                                       new PhotoAgeFriendlyName {Name = "2 days ago", PhotoAge = PhotoAge.DayTwo},
                                                                       new PhotoAgeFriendlyName {Name = "1 week ago", PhotoAge = PhotoAge.WeekOne},
                                                                       new PhotoAgeFriendlyName {Name = "2 weeks ago", PhotoAge = PhotoAge.WeekTwo},
                                                                       new PhotoAgeFriendlyName {Name = "3 weeks ago", PhotoAge = PhotoAge.WeekThree},
                                                                       new PhotoAgeFriendlyName {Name = "1 month ago", PhotoAge = PhotoAge.MonthOne},
                                                                       new PhotoAgeFriendlyName {Name = "2 months ago", PhotoAge = PhotoAge.MonthTwo},
                                                                       new PhotoAgeFriendlyName {Name = "3 months ago", PhotoAge = PhotoAge.MonthThree},
                                                                       new PhotoAgeFriendlyName {Name = "4 months ago", PhotoAge = PhotoAge.MonthFour},
                                                                       new PhotoAgeFriendlyName {Name = "5 months ago", PhotoAge = PhotoAge.MonthFive},
                                                                       new PhotoAgeFriendlyName {Name = "6 months ago", PhotoAge = PhotoAge.MonthSix},
                                                                       new PhotoAgeFriendlyName {Name = "older than 6 months", PhotoAge = PhotoAge.Older},
                                                                   };

        /// <summary>
        /// Gets or sets the recent uploads.
        /// </summary>
        /// <value>The recent uploads.</value>
        public List<RecentUploads> RecentUploads { get; set; }


        /// <summary>
        /// Gets or sets the persistent collection.
        /// </summary>
        /// <value>The persistent collection.</value>
        public IPersistentCollectionService PersistentCollection { private get; set; }

        /// <summary>
        /// Gets or sets the cookie.
        /// </summary>
        /// <value>The cookie.</value>
        public SiteCookie Cookie { private get; set; }

        /// <summary>
        /// Sets the dynamic collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="uploads">The uploads.</param>
        public string SetPersistentCollection(string key, List<RecentUploads> uploads)
        {
            List<Media> media = uploads.ConvertAll(o => (Media) o);

            PersistentCollection.SetBackUrl(string.Format("{0}/recent", Authorization.Owner.Username), Cookie);
            string setKey = PersistentCollection.Set(Authorization.Owner.Username + "_" + key + "_recent", media, Persistence.Permanent);

            return setKey;
        }

        /// <summary>
        /// Gets the recent uploads by photo age.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <returns></returns>
        public List<RecentUploads> GetRecentUploadsByPhotoAge(PhotoAge age)
        {
            return RecentUploads.Where(o => o.Age == age).ToList();
        }

        /// <summary>
        /// Gets the photo age collection.
        /// </summary>
        /// <returns></returns>
        public List<PhotoAgeFriendlyName> GetPhotoAgeCollection()
        {
            return _photoAgeFriendlyNames;
        }

        public class PhotoAgeFriendlyName
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the photo age.
            /// </summary>
            /// <value>The photo age.</value>
            public PhotoAge PhotoAge { get; set; }      
        }
    }
}