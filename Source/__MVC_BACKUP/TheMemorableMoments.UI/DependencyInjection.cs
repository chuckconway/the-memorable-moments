using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;



namespace TheMemorableMoments.UI
{
    public static class DependencyInjection
    {
         //static readonly Container _container = new Container(new TheMemorableMomentsRegistry());

        public static IContainer Container { get; private set; }

        /// <summary>
        /// Initializes the <see cref="DependencyInjection"/> class.
        /// </summary>
        static DependencyInjection()
        {
            var builder = new TheMemorableMomentsContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            Container = builder.Build();
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>()
        {
            return Container.Resolve<TService>();
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>(IEnumerable<KeyValuePair<string,object>> parameters)
        {
            IEnumerable<NamedParameter> namedParameters = parameters.Select(keyValuePair => new NamedParameter(keyValuePair.Key, keyValuePair.Value));
            return Container.Resolve<TService>(namedParameters);
        }
    }
}