using System;
using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.Infrastructure.Repositories
{
	public class ConfigurationRepository : RepositoryBase, IConfigurationRepository
	{		
	
		/// <summary>
		/// Updates the Configuration table by the primary key, if the Configuration is dirty then an update will occur
		/// </summary>
		/// <param name="configuration">a populated configuration</param>
		/// <returns>update count</returns>
		public int Update(Configuration configuration)
		{

			int updateCount = database.NonQuery("Configuration_Update", configuration);
			return updateCount;
		}

		/// <summary>
		/// Retrieves all.
		/// </summary>
		/// <returns></returns>
		public List<Configuration> RetrieveAll() { return database.PopulateCollection("Configuration_SelectAll", database.AutoPopulate<Configuration>); }

		/// <summary>
		/// Retrieves the by primary key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public Configuration RetrieveByPrimaryKey(Guid key)
		{
			List<Configuration> configurations = RetrieveAll();
			return configurations[0];
		}  

	}
}
