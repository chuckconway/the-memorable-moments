namespace TheMemorableMoments.Infrastructure.Data.Queries
{
    public interface IQuery<out T>
    {
        /// <summary>
        /// Retrieves the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        T Retrieve(dynamic values);
    }
}
