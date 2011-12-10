using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Momntz
{
    public interface IPrimaryKey<T>
    {
        T Id { get; }
    }
}
