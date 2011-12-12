using Momntz.Infrastructure;
using Momntz.Infrastructure.Data;
using Momntz.Infrastructure.Data.Command;
using Momntz.Infrastructure.Services.Spam;
using NHibernate;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace Momntz.UI.Web.Injection
{
    public class ApplicationRegistry : Registry 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRegistry"/> class.
        /// </summary>
        public ApplicationRegistry()
        {
            For<IInjection>().Use<StructureMapIoc>();
            For<IProjections>().Use<Projections>();
        }

    }
}