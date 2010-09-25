using System.Collections.Generic;
using Chucksoft.Core.Services;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.UI.Web.Services
{
    public class UserUrlService : IUserUrlService
    {
        private readonly User _user;
        private readonly IConfigurationService _configurationService;
        private readonly string _rootImageUrl;
        private readonly string _rootUrl;
        private readonly string _cloudUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserUrlService"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="user">The user.</param>
        public UserUrlService(IConfigurationService configurationService, User user)
        {
            _configurationService = configurationService;
            _rootImageUrl = _configurationService.GetValueByKey("rootImageServiceUrl");
            _rootUrl = _configurationService.GetValueByKey("rootUrl");
            _cloudUrl = _configurationService.GetValueByKey("cloudUrl");
            _user = user;
        }

        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        public string CreateImageUrl(string username, string relativeToUserRoot)
        {
            //Remove beginning slash
            return CreateImageUrl(relativeToUserRoot, username, _rootImageUrl);
        }

        /// <summary>
        /// Creates the image URL.
        /// </summary>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <param name="username">The username.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private static string CreateImageUrl(string relativeToUserRoot, string username, string url)
        {
            int index = relativeToUserRoot.IndexOf("/");
            if (index == 0)
            {
                relativeToUserRoot = relativeToUserRoot.Remove(0, 1);
            }

            const string format = "{0}/{1}/{2}";
            return string.Format(format, url, username, relativeToUserRoot);
        }

        /// <summary>
        /// Creates the image URL using cloud URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        public string CreateImageUrlUsingCloudUrl(string username, string relativeToUserRoot)
        {
            //Remove beginning slash
            return CreateImageUrl(relativeToUserRoot, username, _cloudUrl);
        }

        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        public string CreateImageUrl(string relativeToUserRoot)
        {
            return CreateImageUrl(_user.Username, relativeToUserRoot);
        }

        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        public string CreateUrl(string username, string relativeToUserRoot)
        {
            //Remove beginning slash
            int index = relativeToUserRoot.IndexOf("/");
            if(index == 0)
            {
                relativeToUserRoot = relativeToUserRoot.Remove(0, 1);
            }                
                        
            const string format = "{0}/{1}/{2}";
            return string.Format(format, _rootUrl, username, relativeToUserRoot);
        }

        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        public string CreateUrl(string relativeToUserRoot)
        {
            return CreateUrl(_user.Username, relativeToUserRoot);
        }

        /// <summary>
        /// Creates the root URL.
        /// </summary>
        /// <param name="relativePathToRoot">The relative path to root.</param>
        /// <returns></returns>
        public string CreateRootUrl(string relativePathToRoot)
        {
            const string format = "{0}/{1}";
            return string.Format(format, _rootUrl, relativePathToRoot);
        }

        /// <summary>
        /// Users the root.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public string UserRoot(string username)
        {
            const string format = "{0}/{1}";
            return string.Format(format, _rootUrl, username);
        }

        /// <summary>
        /// Users the root.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="pathRelativeToUserRoot">The path relative to user root.</param>
        /// <returns></returns>
        public string UserUrl(string username, string pathRelativeToUserRoot)
        {
            const string format = "{0}/{1}/{2}";
            return string.Format(format, _rootUrl, username, pathRelativeToUserRoot);
        }

        /// <summary>
        /// Users the root.
        /// </summary>
        /// <param name="pathRelativeToUserRoot">The path relative to user root.</param>
        /// <returns></returns>
        public string UserUrl(string pathRelativeToUserRoot)
        {
            return UserUrl(_user.Username, pathRelativeToUserRoot);
        }

        /// <summary>
        /// Get the User root Url.
        /// </summary>
        /// <returns></returns>
        public string UserRoot()
        {
            return UserRoot(_user.Username);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static IUserUrlService GetInstance(User user = null)
        {
          return  DependencyInjection.Resolve<IUserUrlService>(new List<KeyValuePair<string, object>>
                                                                           {
                                                                               new KeyValuePair<string, object>("user", user),
                                                                               new KeyValuePair<string, object>("configurationService", DependencyInjection.Resolve<IConfigurationService>())
                                                                           });
        }
    }
}
