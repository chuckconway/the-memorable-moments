using System.Collections.Generic;
using System.Data;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class LocationRepository : RepositoryBase, ILocationRepository
    {
        /// <summary>
        /// Searches the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Location> Search(string text, int userId)
        {
            return database.PopulateCollection("Location_AutocompleteSearch", new { text, userId }, database.AutoPopulate<Location>);
        }

        /// <summary>
        /// Gets the locations by media id.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public List<Location> GetLocationsByMediaId(DataTable ids)
        {
            return database.PopulateCollection("Location_RetrieveByMediaIdCollection", new { ids }, database.AutoPopulate<Location>);
        }

        /// <summary>
        /// Saves the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        public void Save(Location location)
        {
            database.NonQuery("Location_Save", location);
        }
    }
}
