using NHibernate;

namespace Momntz.Infrastructure.Data
{
    public interface IMomntzSessions
    {
        /// <summary>
        /// Gets or sets the momntz.
        /// </summary>
        /// <value>
        /// The momntz.
        /// </value>
        ISession Momntz { get;}

        /// <summary>
        /// Gets or sets the artifact.
        /// </summary>
        /// <value>
        /// The artifact.
        /// </value>
        ISession Artifact { get;}

    }
}
