namespace Momntz.CommandHandlers
{
    public interface ICommandHandler<in T> where T : class
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        void Execute(T command);
    }
}
