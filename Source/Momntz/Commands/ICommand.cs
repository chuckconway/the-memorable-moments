using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Momntz.Commands
{
    public interface ICommand<out T>
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        T Id { get; }
    }
}
