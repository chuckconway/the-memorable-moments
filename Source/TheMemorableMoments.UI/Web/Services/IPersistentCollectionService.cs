using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Web.Services
{
    public interface IPersistentCollectionService
    {
        /// <summary>
        /// Sets the back URL.
        /// </summary>
        /// <param name="backUrl">The back URL.</param>
        /// <param name="cookie">The cookie.</param>
        void SetBackUrl(string backUrl, SiteCookie cookie);

        /// <summary>
        /// Gets the back URL.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        string GetBackUrl(SiteCookie cookie);

        /// <summary>
        /// Sets the specified collection view.
        /// </summary>
        /// <param name="collectionView">The collection view. This is the current collection and view. ie. Year view with the current year being
        /// 1998. The value would be year1998. It must be unique that collection of data.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="persistence">The persistence.</param>
        string Set(string collectionView, List<Media> collection, Persistence persistence);

        /// <summary>
        /// Gets the specified collection key.
        /// </summary>
        /// <param name="collectionKey">The collection key.</param>
        /// <returns></returns>
        string Get(string collectionKey);
    }
}