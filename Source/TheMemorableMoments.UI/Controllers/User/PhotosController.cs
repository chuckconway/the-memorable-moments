using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.Domain.Services;
using TheMemorableMoments.UI.Controllers.ManagePhotos;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Models.Views.Photos;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class PhotosController : PhotoBaseController
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IImageService _rotateService;
        private readonly IMediaFileService _mediaFileService;
        private readonly ILocationRepository _locationRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="PhotosController"/> class.
        /// </summary>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="tagRepository">The tag repository.</param>
        /// <param name="rotateService">The rotate service.</param>
        /// <param name="managePhotoService">The manage photo service.</param>
        /// <param name="mediaFileService">The media file service.</param>
        /// <param name="locationRepository">The location repository.</param>
        /// <param name="albumRepository">The album repository.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
        public PhotosController(IMediaRepository mediaRepository,
            ITagRepository tagRepository,
            IImageService rotateService,
            IManagePhotoFactory managePhotoService,
            IMediaFileService mediaFileService, ILocationRepository locationRepository, IAlbumRepository albumRepository, IPersistentCollectionService persistentCollectionService)
            : base(managePhotoService, persistentCollectionService)
        {
            _mediaRepository = mediaRepository;
            _albumRepository = albumRepository;
            _locationRepository = locationRepository;
            _mediaFileService = mediaFileService;
            _rotateService = rotateService;
            _tagRepository = tagRepository;

        }

        /// <summary>
        /// Manages the photos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Index(string cp)
        {
            List<Media> media = _mediaRepository.RetrievePublic(Owner.Id);
            return PhotoView("public", cp, media);
        }

        /// <summary>
        /// Photoes the specified username.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        public ActionResult Show(string set)
        {
            string mediaKeys = (string.IsNullOrEmpty(set) ? string.Empty : persistentCollectionService.Get(set));
            string url = persistentCollectionService.GetBackUrl(SiteCookie);
            string backlink = (url != null ? UrlService.CreateRootUrl(url) : string.Empty);

            PhotoView photoView = ModelFactory<PhotoView>(new { backlink, mediaKeys, setId = set });
            return View(photoView);
        }


        /// <summary>
        /// Manages the photos.
        /// </summary>
        /// <param name="postView">The post view.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Index(ManagePhotoPostView postView)
        {
            List<Media> list = _mediaRepository.RetrievePublic(Owner.Id);
            return PhotoView("public", postView, list);
        }


        /// <summary>
        /// Publics the specified cp.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Public(string cp)
        {
            List<Media> list = _mediaRepository.RetrievePublic(Owner.Id);
            return PhotoView("public", cp, list);
        }

        /// <summary>
        /// Manages the photos.
        /// </summary>
        /// <param name="postView">The post view.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Public(ManagePhotoPostView postView)
        {
            List<Media> media = _mediaRepository.RetrievePublic(Owner.Id);
            return PhotoView("public", postView, media);
        }

        /// <summary>
        /// Publics the specified cp.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult InNetwork(string cp)
        {
            List<Media> list = _mediaRepository.RetrieveInNetwork(Owner.Id);
            return PhotoView("innetwork", cp, list);
        }

        /// <summary>
        /// Searches the specified cp.
        /// </summary>
        /// <param name="postView">The post view.</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Search(ManagePhotoPostView postView)
        {
            List<Media> search = new List<Media>(); 
            if (postView != null && !string.IsNullOrEmpty(postView.Text))
            {
                search = _mediaRepository.SearchByTextAndUserId(postView.Text, Owner.Id);
            }

// ReSharper disable PossibleNullReferenceException
            postView.Submit = "search";
// ReSharper restore PossibleNullReferenceException
            
            return PhotoView("search", postView, search);
        }

        /// <summary>
        /// Manages the photos.
        /// </summary>
        /// <param name="postView">The post view.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult InNetwork(ManagePhotoPostView postView)
        {
            List<Media> list = _mediaRepository.RetrieveInNetwork(Owner.Id);
            return PhotoView("innetwork", postView, list);
        }

        /// <summary>
        /// Publics the specified cp.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Private(string cp)
        {
            List<Media> list = _mediaRepository.RetrievePrivate(Owner.Id);
            return PhotoView("private", cp, list);
        }

        /// <summary>
        /// Manages the photos.
        /// </summary>
        /// <param name="postView">The post view.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Private(ManagePhotoPostView postView)
        {
            List<Media> list = _mediaRepository.RetrievePrivate(Owner.Id);
            return PhotoView("private", postView, list);
        }

        /// <summary>
        /// Edits the specified username.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Edit(string set)
        {
            string mediaKeys = (string.IsNullOrEmpty(set) ? string.Empty : persistentCollectionService.Get(set));
            EditPhotoView view = new EditPhotoView { MediaKeys = mediaKeys };
            view = SetAuthorizationAndUrlService(view);
            return View(view);
        }

        /// <summary>
        /// Gets the edit details.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult GetEditDetails(int id)
        {
            Media media = _mediaRepository.RetrieveByPrimaryKeyAndUserId(id, Authorization.Owner.Id);
            MediaFile file = media.GetImageByPhotoType(PhotoType.Websize);
            string imageUrl = UrlService.CreateImageUrl(file.FilePath);
            var data = new
                           {
                               ImageUrl = imageUrl,
                               media.Title,
                               Story = media.Description,
                               media.Year,
                               media.Month,
                               media.Day,
                               LocationName = (media.Location.LocationName ?? string.Empty),
                               Latitude = (media.Location.Latitude == 0 ? string.Empty : media.Location.Latitude.ToString()),
                               Longitude = (media.Location.Longitude == 0 ? string.Empty : media.Location.Longitude.ToString()),
                               Zoom = (media.Location.Zoom == 0 ? string.Empty : media.Location.Zoom.ToString()),
                               media.Location.MapTypeId, 
                               Tags = (media.Tags == "untagged" ? string.Empty : media.Tags),
                               media.BelongsToAlbums,
                               Status = media.Status.ToString()
                            };

            return Json(data);
        }

        /// <summary>
        /// Saves the photo details.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="details">The details.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePhotoDetails(int id, EditPhotoDetails details)
        {
            Media media = Mapper.Map<EditPhotoDetails, Media>(details);
            media.MediaId = id;
            media.Owner = new Owner {UserId = Authorization.Owner.Id};
            _mediaRepository.Save(media);

            if(!string.IsNullOrEmpty(details.SelectedAlbums))
            {
                string[] strings = details.SelectedAlbums.Split(',');
                int[] ids = strings.Where(o => !string.IsNullOrEmpty(o)).Select(o => Convert.ToInt32(o)).ToArray();
                _albumRepository.AddAlbumsToPhoto(id, ids);
            }

            
            return Content("1");
        }
        
        /// <summary>
        /// Edits the specified username.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="savedPhotoView">The saved photo view.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Media media, SavedPhotoView savedPhotoView)
        {
            media = Mapper.Map(savedPhotoView, media);
            _mediaRepository.Save(media);

            EditPhotoView view = new EditPhotoView { UIMessage = "Photo details saved."};
            view = SetAuthorizationAndUrlService(view);
            return View(view);
        }


        /// <summary>
        /// Tags the search.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <returns></returns>
        public ActionResult TagSearch(string q)
        {
            List<Tag> tags = _tagRepository.Search(Owner.Id, q);
            StringBuilder builder = new StringBuilder();
            tags.ForEach(o => builder.AppendLine(string.Format("{0}|{1}", o.TagText, o.TagText.Replace(q, "<strong>" + q + "</strong>"))));

            return Content(builder.ToString());
        }

        /// <summary>
        /// Metas the specified id.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult Meta(Media media)
        {
            string content = PhotoHtmlHelper.GetPhotoDetailLinks(media, Authorization.IsOwner);
            return Content(content);
        }

        /// <summary>
        /// Fullsizes the specified media id.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult Fullsize(Media media)
        {
            MediaFile fullsizeFile = media.GetImageByPhotoType(PhotoType.Original);
            return GetFile(fullsizeFile);
        }

        /// <summary>
        /// Fullsizes the specified media id.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult Medium(Media media)
        {
            MediaFile fullsizeFile = media.GetImageByPhotoType(PhotoType.Websize);
            return GetFile(fullsizeFile);
        }

        /// <summary>
        /// Fullsizes the specified media id.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult Small(Media media)
        {
            MediaFile fullsizeFile = media.GetImageByPhotoType(PhotoType.Thumbnail);
            return GetFile(fullsizeFile);
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="fullsizeFile">The fullsize file.</param>
        /// <returns></returns>
        private ActionResult GetFile(MediaFile fullsizeFile)
        {
            string url = UrlService.CreateImageUrlUsingCloudUrl(Owner.Username, fullsizeFile.FilePath);

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            // ReSharper disable PossibleNullReferenceException
            Stream stream = response.GetResponseStream();
            // ReSharper restore PossibleNullReferenceException

            return File(stream, "application/octet-stream", fullsizeFile.OriginalFileName);
        }

        /// <summary>
        /// Gets the media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public JsonResult Details(Media media)
        {
            string linkHtml = PhotoHtmlHelper.GetPhotoDetailLinks(media, Authorization.IsOwner);
            return Json(new { detailHtml = linkHtml, mediaTitle = media.Title, mediaDescription = media.Description }, "application/json", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public JsonResult GetDetails(Media media)
        {
            MediaFile file = media.GetImageByPhotoType(PhotoType.Websize);
            string imageSrc = UrlService.CreateImageUrl(file.FilePath);
            string details = GetDetailSection(media, Authorization.IsOwner);
            return Json(new { imageSrc, media.Title, media.Description, details, Authorization.Owner.DisplayName}, "application/json");
        }

        /// <summary>
        /// Rotates the left.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult RotateLeft(Media media)
        {
            int status = Rotate(media, _rotateService.RotateLeft);
            return Content(status.ToString());
        }

        /// <summary>
        /// Rotates the specified username.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="resizeMethod">The resize method.</param>
        /// <returns></returns>
        private int Rotate(IMediaFiles media, Action<List<MediaFile>, Domain.Model.User> resizeMethod)
        {
            resizeMethod(media.MediaFiles, Owner);
            const int status = 1;
            return status;
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            _mediaFileService.DeleteMedia(Owner, id);
            return Content("0");
        }

        /// <summary>
        /// Rotates the right.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public ActionResult RotateRight(Media media)
        {
            int status = Rotate(media, _rotateService.RotateRight);
            return Content(status.ToString());
        }
        
        /// <summary>
        /// Tags the search.
        /// </summary>
        /// <param name="name_startsWith">The name_starts with.</param>
        /// <returns></returns>
// ReSharper disable InconsistentNaming
        public ActionResult SearchLocations(string name_startsWith)
// ReSharper restore InconsistentNaming
        {
            List<Location> locations = _locationRepository.Search(name_startsWith, Owner.Id);
            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the photo detail links.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <returns></returns>
        private string GetDetailSection(Media media, bool isAuthenticated)
        {
            ITagService tagService = DependencyInjection.Resolve<ITagService>();

            string html = @"<ul>
                        <li>
                        <span>";
            html += (isAuthenticated ? @"<a id=""editlink""  href=""{0}/photos/edit/{1}"">edit</a>" : string.Empty);
            html += @"</span>            
                    </li>";
            html += "{2}";
            html += @"          
                    </li>
                     {3}        
                    <li><span><a href=""{0}/comments/leave/{1}"">comments ({4})</a></span> 
                </ul>";

            string tags = string.Empty;
            if (!string.IsNullOrEmpty(media.Tags))
            {
                const string tagFormat = @"<li><span>tags:</span> {0}</li>";
                string renderedTags = tagService.HyperlinkTheTags(media.Tags, media.Owner.Username);
                tags = string.Format(tagFormat, renderedTags);
            }

            string date = PhotoHtmlHelper.GetDate(media);
            string content = string.Format(html, UrlService.UserRoot(media.Owner.Username), HttpUtility.HtmlEncode(media.MediaId.ToString()), date, tags, media.CommentCount); return content;
        }

    }
}
