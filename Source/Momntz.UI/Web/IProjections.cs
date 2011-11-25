namespace Momntz.UI.Web
{
    public interface IProjections
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();

        /// <summary>
        /// Gets the specified values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        T Get<T>(dynamic values);
    }
}