using System.Collections.Generic;
using Momntz.Domain.Model;

namespace Momntz.Infrastructure.Projections
{
    public class HomeIndexProjection
    {
        public IList<Momento> Media { get; set; }
    }
}
