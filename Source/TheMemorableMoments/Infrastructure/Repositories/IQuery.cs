using System.Collections.Generic;
using System.Data.Common;

namespace ThePhotoProject.Infrastructure.Repositories
{
    public interface IQuery
    {
        /// <summary>
        /// Gets the procedure.
        /// </summary>
        /// <value>The procedure.</value>
        string Procedure { get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        List<DbParameter> Parameters { get; }
    }
}