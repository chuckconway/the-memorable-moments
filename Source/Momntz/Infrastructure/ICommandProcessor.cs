namespace Momntz.Infrastructure
{
    public interface ICommandProcessor
    {
        /// <summary>
        /// Processes the specified command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        void Process<T>(T command) where T : class;
    }
}
