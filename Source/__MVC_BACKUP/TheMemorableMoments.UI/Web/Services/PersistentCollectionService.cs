using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chucksoft.Core.Cryptography;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.UI.Web.Services
{
    public class PersistentCollectionService : IPersistentCollectionService
    {
        private readonly IPersistentCollectionRepository _persistentCollectionRepository;
        private const string _backUrlCookieName = "backurl";


        /// <summary>
        /// Initializes a new instance of the <see cref="PersistentCollectionService"/> class.
        /// </summary>
        /// <param name="persistentCollectionRepository">The persistent collection repository.</param>
        public PersistentCollectionService(IPersistentCollectionRepository persistentCollectionRepository)
        {
            _persistentCollectionRepository = persistentCollectionRepository;
        }

        /// <summary>
        /// Sets the back URL.
        /// </summary>
        /// <param name="backUrl">The back URL.</param>
        /// <param name="cookie">The cookie.</param>
        public void SetBackUrl(string backUrl, SiteCookie cookie)
        {
            cookie.Set(_backUrlCookieName, HttpUtility.UrlEncode(backUrl));
        }

        /// <summary>
        /// Gets the back URL.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        public string GetBackUrl(SiteCookie cookie)
        {
            return HttpUtility.UrlDecode(cookie.Get(_backUrlCookieName));
        }

        /// <summary>
        /// Sets the specified collection view.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="persistence">The persistence.</param>
        public string Set(string key, List<Media> collection, Persistence persistence)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(key);
            string encodedKey = HexEncoding.ToString(bytes);



            IEnumerable<int> ids = collection.Select(m => m.MediaId);
            string idCollection = string.Join(",", ids);

            PersistentCollection collection1 = _persistentCollectionRepository.Get(key);

            if (collection1 == null || collection1.Value != idCollection)
            {
                PersistentCollection persistentCollection = new PersistentCollection
                                                          {
                                                              CollectionKey = key,
                                                              Persistence = persistence,
                                                              Value = idCollection
                                                          };

                _persistentCollectionRepository.Set(persistentCollection);
            }

            return encodedKey;
        }

        /// <summary>
        /// Gets the specified collection key.
        /// </summary>
        /// <param name="collectionKey">The collection key.</param>
        /// <returns></returns>
        public string Get(string collectionKey)
        {
            byte[] bytes = HexEncoding.GetBytes(collectionKey);
            string encodedKey = System.Text.Encoding.Default.GetString(bytes);

            //string decryptHexEncoded = _cryptographyService.Decrypt(collectionKey);
            PersistentCollection collection = _persistentCollectionRepository.Get(encodedKey);
            return (collection != null ? collection.Value : string.Empty);
        }

    }
}