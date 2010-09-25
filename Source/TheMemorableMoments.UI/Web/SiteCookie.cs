using System.Web;
using Chucksoft.Core.Services;

namespace TheMemorableMoments.UI.Web
{
    public class SiteCookie
    {
        private readonly HttpContextBase _contextBase;
        private readonly ICryptographyService _cryptographyService;
        private const string _cookieName = "moments";

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteCookie"/> class.
        /// </summary>
        /// <param name="contextBase">The context base.</param>
        /// <param name="cryptographyService">The cryptography service.</param>
        public SiteCookie(HttpContextBase contextBase, ICryptographyService cryptographyService)
        {
            _contextBase = contextBase;
            _cryptographyService = cryptographyService;
        }
        

        /// <summary>
        /// Retrieves the values.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Get(string key)
        {
            string val = string.Empty;
            HttpCookie cookie = _contextBase.Request.Cookies[_cookieName];

            if(cookie != null)
            {
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    cookie = new HttpCookie(_cookieName, _cryptographyService.Decrypt(cookie.Value));
                    val = cookie[key];
                }
            }

            return val;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="val">The val.</param>
        public void Set(string key, string val)
        {
            HttpCookie cookie = _contextBase.Items[_cookieName] as HttpCookie ?? 
                (_contextBase.Request.Cookies[_cookieName] ?? 
                new HttpCookie(_cookieName));
            
            if (!string.IsNullOrEmpty(cookie.Value))
            {
                cookie = new HttpCookie(_cookieName, _cryptographyService.Decrypt(cookie.Value));    
            }

            cookie.Values.Remove(key);
            cookie.Values[key] = val;
            
            _contextBase.Response.Cookies[_cookieName].Value = _cryptographyService.Encrypt(cookie.Value);
            _contextBase.Items[_cookieName] = _contextBase.Response.Cookies[_cookieName];
        }
    }
}
