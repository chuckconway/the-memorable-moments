using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Model.Recent
{
    public class RecentUploads: Media
    {
        public PhotoAge Age { get; set; }
    }
}
