namespace TheMemorableMoments.UI.Web.Services
{
    public interface IUserUrlService
    {
        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        string CreateUrl(string username, string relativeToUserRoot);

        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        string CreateUrl(string relativeToUserRoot);

        /// <summary>
        /// Users the root.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        string UserRoot(string username);

        /// <summary>
        /// Users the root.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="pathRelativeToUserRoot">The path relative to user root.</param>
        /// <returns></returns>
        string UserUrl(string username, string pathRelativeToUserRoot);

        /// <summary>
        /// Users the root.
        /// </summary>
        /// <param name="pathRelativeToUserRoot">The path relative to user root.</param>
        /// <returns></returns>
        string UserUrl(string pathRelativeToUserRoot);

        /// <summary>
        /// Get the User root Url.
        /// </summary>
        /// <returns></returns>
        string UserRoot();

        /// <summary>
        /// Creates the image URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        string CreateImageUrl(string username, string relativeToUserRoot);

        /// <summary>
        /// Creates the image URL.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        string CreateImageUrl(string relativeToUserRoot);

        /// <summary>
        /// Creates the root URL.
        /// </summary>
        /// <param name="relativePathToRoot">The relative path to root.</param>
        /// <returns></returns>
        string CreateRootUrl(string relativePathToRoot);

        /// <summary>
        /// Creates the image URL using cloud URL.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="relativeToUserRoot">The relative to user root.</param>
        /// <returns></returns>
        string CreateImageUrlUsingCloudUrl(string username, string relativeToUserRoot);
    }
}