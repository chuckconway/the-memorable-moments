using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public class TrackMediaViewsService : ITrackMediaViewsService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaViewsRepository _mediaViewsRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackMediaViewsService"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="mediaViewsRepository">The media views repository.</param>
        public TrackMediaViewsService(IMediaRepository mediaRepository, IMediaViewsRepository mediaViewsRepository)
        {
            _mediaRepository = mediaRepository;
            _mediaViewsRepository = mediaViewsRepository;
        }

        /// <summary>
        /// Retrieves the by primary key and user id.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public Media RetrieveByPrimaryKeyAndUserId(int mediaId, int userId)
        {
            _mediaViewsRepository.UpdateCount(mediaId, userId);
            return _mediaRepository.RetrieveByPrimaryKeyAndUserId(mediaId, userId);
        }
    }
}
