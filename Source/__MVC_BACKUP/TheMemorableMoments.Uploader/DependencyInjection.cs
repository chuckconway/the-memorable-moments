using Autofac;

namespace TheMemorableMoments.Uploader
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
    }
}