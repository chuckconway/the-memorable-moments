using NHibernate;

namespace Momntz.Infrastructure.Data
{
    public class Databases : IMomntzSessions
    {
        /// <summary>
        /// Gets or sets the momntz.
        /// </summary>
        /// <value>
        /// The momntz.
        /// </value>
        public ISession Momntz { get; private set; }

        /// <summary>
        /// Gets or sets the artifact.
        /// </summary>
        /// <value>
        /// The artifact.
        /// </value>
        public ISession Artifact { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Databases"/> class.
        /// </summary>
        public Databases(IMomntzSessionFactories sessionFactories)
        {
            Momntz = sessionFactories.GetMomntzSession().OpenSession();
            Artifact = sessionFactories.GetArtifactSession().OpenSession();
        }
    }
}
