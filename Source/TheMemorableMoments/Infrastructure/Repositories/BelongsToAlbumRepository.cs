using System.Collections.Generic;
using System.Data;
using TheMemorableMoments.Domain.Model.Albums;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class BelongsToAlbumRepository : RepositoryBase, IBelongsToAlbumRepository
    {        /// <summary>
        /// Retrieves the by media ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public List<BelongsToAlbum> RetrieveByMediaIds(DataTable ids)
        {
            return database.PopulateCollection("BelongsToAlbum_RetrieveByMediaIds", new { ids }, database.AutoPopulate<BelongsToAlbum>);
        }
    }
}