using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMemorableMoments.Domain.Handlers
{
    interface IHandles<in T>
    {
        void Handle(T cmd);
    }
}
