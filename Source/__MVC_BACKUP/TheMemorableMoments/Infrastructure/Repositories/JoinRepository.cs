namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class JoinRepository : RepositoryBase, IJoinRepository
    {
        /// <summary>
        /// Tokens the count.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public int GetTokenCount(string token)
        {
            return (int)database.Scalar("Token_RetreiveCount", new {token});
        }
    }
}
