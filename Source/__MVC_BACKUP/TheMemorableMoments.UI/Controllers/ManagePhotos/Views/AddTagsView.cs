using System;
using System.Collections.Generic;
using System.Linq;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.Domain.Services;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.ManagePhotos.Views
{
    public class AddTagsView : ManagePhotoBase, IRenderManagePhotoView
    {
        private readonly IUpdateTagsService _updateTagService;
        private readonly ITagService _tagService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTagsView"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="tagService">The tag service.</param>
        /// <param name="authorization">The authorization.</param>
        public AddTagsView(Domain.Model.User user, ITagService tagService, Authorization authorization)
            : base(user, authorization)
        {
            _updateTagService = DependencyInjection.Resolve<IUpdateTagsService>();
            _tagService = tagService;
        }

        /// <summary>
        /// Gets the name of the button.
        /// </summary>
        /// <value>The name of the button.</value>
        public string ButtonName
        {
            get { return "add tags"; }
        }

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <param name="viewLink">The view link.</param>
        /// <param name="postView">The post view.</param>
        /// <param name="retrieve"></param>
        /// <returns></returns>
        public ManagePhotosView RenderView(string viewLink, ManagePhotoPostView postView, Func<List<Media>> retrieve)
        {
            AddTagsToMedia(postView);

            ManagePhotosView managePhotosView = RenderMedia(postView, retrieve, "ManagedPhotoView");
            managePhotosView.UIMessage = (postView.MediaId != null ? string.Format("Tags were successfully added to {0} photo(s)", postView.MediaId.Length) : string.Empty);
            managePhotosView.BreadCrumbs = SetBreadCrumb(viewLink);

            managePhotosView.TabName = viewLink;
            managePhotosView.RenderAdminTags = _tagService.HyperlinkTheAdminTags;
            managePhotosView.Authorization = authorization;

            return managePhotosView;
        }

        /// <summary>
        /// Adds the tags to media.
        /// </summary>
        /// <param name="postView">The post view.</param>
        private void AddTagsToMedia(ManagePhotoPostView postView)
        {
            TagCollection tagCollection = new TagCollection(postView.Tags);
            List<Media> mediae = mediaRepository.RetrieveByMediaIds(postView.MediaId, user.Id);

            if (postView.MediaId != null)
            {
                foreach (int id in postView.MediaId)
                {
                    int mediaId = id;
                    Media media = mediae.Where(o => mediaId == o.MediaId).First();

                    TagCollection currentTags = new TagCollection(media.Tags);
                    IEnumerable<Tag> newTags = JoinTags(tagCollection, currentTags);
                    media.AddTags(newTags);

                    _updateTagService.UpdateTags(media.Tags, mediaId, user);
                }
            }
        }

        /// <summary>
        /// Gets the join tags.
        /// </summary>
        /// <param name="newCollection">The new collection.</param>
        /// <param name="currentCollection">The current collection.</param>
        /// <returns></returns>
        private static IEnumerable<Tag> JoinTags(TagCollection newCollection, TagCollection currentCollection)
        {
            return (from tag in newCollection.Tags
                    let tag1 = tag
                    let found = currentCollection.Tags.Any(media1 => string.Equals(media1.TagText, tag1.TagText, StringComparison.InvariantCultureIgnoreCase))
                    where !found
                    select tag).ToList();
        }

    }

   
}