using System.Collections.Generic;
using System.Data;
using TheMemorableMoments.Domain.Model.Albums;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public interface IBelongsToAlbumRepository
    {
        /// <summary>
        /// Retrieves the by media ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        List<BelongsToAlbum> RetrieveByMediaIds(DataTable ids);
    }
}