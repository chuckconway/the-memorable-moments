using System.Collections.Generic;
using System.Data;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments
{
    public interface ILocationRepository
    {
        /// <summary>
        /// Searches the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Location> Search(string text, int userId);

        /// <summary>
        /// Saves the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        void Save(Location location);

        /// <summary>
        /// Gets the locations by media id.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        List<Location> GetLocationsByMediaId(DataTable ids);
    }
}