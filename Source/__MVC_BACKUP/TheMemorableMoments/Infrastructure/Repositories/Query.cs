using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ThePhotoProject.Infrastructure.Repositories
{
    public class Query : MsSqlQueryBase, IQuery
    {
        /// <summary>
        /// Occurs when [on after parameter generation].
        /// </summary>
        public event Action<List<DbParameter>> OnAfterParameterGeneration = delegate { };

        /// <summary>
        /// Occurs when [on after execution].
        /// </summary>
        public event Action<List<DbParameter>> OnAfterExecution = delegate { };

        /// <summary>
        /// Gets or sets the procedure.
        /// </summary>
        /// <value>The procedure.</value>
        public string Procedure { get; set; }

        /// <summary>
        /// Sets the parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        public void SetParameters<T>(T parameters) where T: class
        {
            List<DbParameter> dbParameters = GetParameters(parameters);
            Parameters = dbParameters;
        }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public List<DbParameter> Parameters { get; private set;}
    }
}
