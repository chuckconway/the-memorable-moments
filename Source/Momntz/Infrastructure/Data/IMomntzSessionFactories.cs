using NHibernate;

namespace Momntz.Infrastructure.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMomntzSessionFactories
    {
        /// <summary>
        /// Gets the artifact session.
        /// </summary>
        /// <returns></returns>
        ISessionFactory GetArtifactSession();

        /// <summary>
        /// Gets the momntz session.
        /// </summary>
        /// <returns></returns>
        ISessionFactory GetMomntzSession();
    }
}