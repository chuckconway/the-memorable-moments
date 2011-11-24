using System.Collections.Generic;
using System.Data.Common;
using Chucksoft.Core.Data;
using Chucksoft.Core.Data.Services;

namespace ThePhotoProject.Infrastructure.Repositories
{
    public abstract class MsSqlQueryBase
    {
        private readonly IParameterBuilder _parameterBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBase"/> class.
        /// </summary>
        protected MsSqlQueryBase()
        {
            _parameterBuilder = new MsSqlDatabase();
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public List<DbParameter> GetParameters<T>(T parameters)
        {
            return _parameterBuilder.GetParameters(parameters);
        }
    }
}
