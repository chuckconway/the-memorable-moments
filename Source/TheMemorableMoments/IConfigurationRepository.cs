using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments
{
    public interface IConfigurationRepository
    {

        /// <summary>
        /// Updates the Configuration table by the primary key, if the Configuration is dirty then an update will occur
        /// </summary>
        /// <param name="configuration">a populated configuration</param>
        /// <returns>update count</returns>
        int Update(Configuration configuration);

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        List<Configuration> RetrieveAll();

        /// <summary>
        /// Retrieves the by primary key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Configuration RetrieveByPrimaryKey(Guid key);
    }
}