using System.Collections.Generic;
using Momntz.Infrastructure.Data.DTOs;

namespace Momntz.Infrastructure.Projections
{
    public class HomeIndexProjection
    {
        public IList<Momento> Media { get; set; }
    }
}
