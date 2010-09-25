using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;

namespace TheMemorableMoments.Domain.Services
{
    public class UpdateTagsService : IUpdateTagsService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMediaRepository _mediaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTagsService"/> class.
        /// </summary>
        /// <param name="tagRepository">The tag repository.</param>
        /// <param name="mediaRepository">The media repository.</param>
        public UpdateTagsService(ITagRepository tagRepository, IMediaRepository mediaRepository)
        {
            _tagRepository = tagRepository;
            _mediaRepository = mediaRepository;
        }

        /// <summary>
        /// Updates the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="mediaId">The media id.</param>
        /// <param name="user">The user.</param>
        public void UpdateTags(string tags, int mediaId, User user)
        {
            _tagRepository.Update(tags, mediaId);

            Media media = _mediaRepository.RetrieveByPrimaryKeyAndUserId(mediaId, user.Id);
            media.Tags = tags;

            _mediaRepository.Save(media);
        }
    }
}
