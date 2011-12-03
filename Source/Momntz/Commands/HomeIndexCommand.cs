using System;

namespace Momntz.Commands
{
    public class HomeIndexCommand : ICommand<Guid>
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeIndexCommand"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public HomeIndexCommand(Guid id)
        {
            Id = id;
        }
    }
}
