namespace TheMemorableMoments
{
    public interface IJoinRepository
    {
        /// <summary>
        /// Tokens the count.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        int GetTokenCount(string token);
    }
}