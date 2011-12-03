using Momntz.CommandHandlers;

namespace Momntz.Infrastructure
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IInjection _injection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Projections"/> class.
        /// </summary>
        /// <param name="injection">The injection.</param>
        public CommandProcessor(IInjection injection)
        {
            _injection = injection;
        }

        /// <summary>
        /// Processes the specified command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        public void Process<T>(T command) where T : class
        {
            var commandHandler = _injection.Get<ICommandHandler<T>>();
            commandHandler.Execute(command);
            
        }
    }
}
