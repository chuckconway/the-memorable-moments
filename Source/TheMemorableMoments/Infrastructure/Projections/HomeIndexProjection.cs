using System.Collections.Generic;
using TheMemorableMoments.Infrastructure.Data.DTOs;

namespace TheMemorableMoments.Infrastructure.Projections
{
    public class HomeIndexProjection
    {
        public IList<Media> Media { get; set; }
    }
}
