using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.Albums;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Domain.Model.Tags;
using TheMemorableMoments.UI.Models;
using TheMemorableMoments.UI.Models.Views.AlbumModels;
using TheMemorableMoments.UI.Web.Controllers;
using TheMemorableMoments.UI.Web.Controls.TreeView;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers.User
{
	public class AlbumsController : HybridController
	{
		private readonly IAlbumRepository _albumRepository;
		private readonly IMediaRepository _mediaRepository;
        private readonly IPaginationService<Media> _paginationService;
        private readonly IPersistentCollectionService _persistentCollectionService;

		private const string _removePhotosFromAlbumMessageCookieKey = "RemovePhotoMessage";

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumsController"/> class.
        /// </summary>
        /// <param name="albumRepository">The album repository.</param>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="paginationService">The pagination service.</param>
        /// <param name="persistentCollectionService">The persistent collection service.</param>
		public AlbumsController(IAlbumRepository albumRepository, 
            IMediaRepository mediaRepository,
            IPaginationService<Media> paginationService, IPersistentCollectionService persistentCollectionService)
		{
			_albumRepository = albumRepository;
            _persistentCollectionService = persistentCollectionService;
            _paginationService = paginationService;
			_mediaRepository = mediaRepository;
		}

		/// <summary>
		/// Gets the landing page.
		/// </summary>
		/// <returns></returns>
		public ActionResult GetLandingPage()
		{
			//0 - albumId
			//1 - title
			//2 - randomImage

			const string template = @"<li class=""album"">
                    <span class=""border"">
					 <span class=""image"">
						<a name=""{0}"" href=""#/show/{0}"" title=""{1}""> 
							<img src=""{2}"" alt=""Title #0"" />
						</a>                        
					  </span> 
                    </span>
					  <span class=""albumtitle"" >{1}</span>
					</li>";

            List<Album> topAlbums = _albumRepository.RetrieveTopLevelAlbumsByUserId(Owner.Id);
			List<AlbumLandingPageDecorator> landingPageDecorators = topAlbums.ConvertAll(o =>
			{
				Media media = (o.Media != null ? o.Media.FirstOrDefault() : null);
				return new AlbumLandingPageDecorator(o) { RandomImage = GetRandomAlbumImage(media) };
			});

			StringBuilder stringBuilder = new StringBuilder();
			landingPageDecorators.ForEach(o => stringBuilder.AppendLine(string.Format(template, o.Album.AlbumId, o.Album.Name, o.RandomImage)));
			return Json(new { albums = stringBuilder.ToString(), count = topAlbums.Count, isAdmin = Authorization.IsOwner }, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Deletes the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Delete(int id)
		{
            _albumRepository.Delete(id, Owner.Id);
			return Content("0");
		}

        /// <summary>
        /// Manages the specified id.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns></returns>
        public ActionResult Photos(Album album)
        {
            List<MediaWithAlbumPosition> media = _mediaRepository.GetMediaWithAlbumPositionByAlbumId(album.AlbumId, Owner.Id);
            string albumCrumbs = GetAlbumCrumbs(album);

            IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            breadCrumbs.Add(album.Name, string.Empty);
            breadCrumbs.Add("photos", string.Empty);

            string setKey = _persistentCollectionService.Set(Authorization.Owner.Username + "_ManageAlbum_" + album.AlbumId, media.ConvertAll(o => (Media)o), Persistence.Permanent);
            _persistentCollectionService.SetBackUrl(Authorization.Owner.Username + "/albums/photos/" + album.AlbumId, SiteCookie);

            ManagePhotosView manageView = ModelFactory<ManagePhotosView>(new {album, media, albumCrumbs, Set=setKey});
            return View(manageView, breadCrumbs);
        }

        

        /// <summary>
        /// Manages the specified id.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(Album album)
        {
            string albumCrumbs = GetAlbumCrumbs(album);
            IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            breadCrumbs.Add(album.Name, UrlService.UserUrl("albums/#/show/" + album.AlbumId));
            breadCrumbs.Add("details", string.Empty);
            
            string message = SiteCookie.Get(_removePhotosFromAlbumMessageCookieKey);

            if (!string.IsNullOrEmpty(message))
            {
                SiteCookie.Set(_removePhotosFromAlbumMessageCookieKey, string.Empty);
            }
            
            Media media = null;
            
            if(album.CoverMediaId.HasValue)
            {
                media = _mediaRepository.RetrieveByPrimaryKeyAndUserId(album.CoverMediaId.GetValueOrDefault(), Owner.Id);
            }

            ManageDetailsView manageView = ModelFactory<ManageDetailsView>(new{ Album = album, CoverMedia = media, albumCrumbs, UIMessage = message});
            return View(manageView, breadCrumbs);
        }

		/// <summary>
		/// Edits the specified album id.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <param name="name">The name.</param>
		/// <param name="description">The description.</param>
		/// <returns></returns>
		[HttpPost]
        public ActionResult Details(Album album, string name, string description)
		{
            ManageDetailsView manageView = ModelFactory<ManageDetailsView>();
			album.Description = description ?? string.Empty;
			album.Name = name;
			_albumRepository.Save(album);

            IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            breadCrumbs.Add(album.Name, UrlService.UserUrl("albums/#/show/" + album.AlbumId));
            breadCrumbs.Add("details", string.Empty);
            
            manageView.Album = album;
            manageView.AlbumCrumbs = GetAlbumCrumbs(album);
			manageView.UIMessage =  album.Name + " saved.";

            return View(manageView, breadCrumbs);
		}


		/// <summary>
		/// Updates the photo position.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="ids">The ids.</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult UpdatePhotoPosition(int id, int[] ids)
		{
			List<MediaPosition> positions = ids.Select((t, index) => new MediaPosition { MediaId = t, Position = index + 1 }).ToList();
            _albumRepository.UpdateMediaPosition(Owner.Id, id, positions);
			return Content("");
		}

		/// <summary>
		/// Shows all.
		/// </summary>
		/// <returns></returns>
        [ActionName("AddPhotos")]
		public ActionResult All(Album album, string cp)
		{
			IDictionary<string, string> breadCrumbs = GetBreadCrumbs(album);
			breadCrumbs.Add("viewing all", string.Empty);
		    List<Media> media = _mediaRepository.RetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserId(album.AlbumId, Owner.Id);

		    Func<List<Media>> retrieveMedia = () => (media);
            BySearchingView albumView = GetAlbumView(album, cp, retrieveMedia);

            albumView.Set = _persistentCollectionService.Set(Authorization.Owner.Username + "_AddPhotosAll_" + album.AlbumId, media, Persistence.Permanent);
            _persistentCollectionService.SetBackUrl(Authorization.Owner.Username + "/albums/addphotos/" + album.AlbumId, SiteCookie);

            albumView.PartialViewName = "All";
			return View(albumView, breadCrumbs);
		}

        /// <summary>
        /// Sets the cover image.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        public ActionResult SetCoverImage(int mediaId, int albumId)
        {
            _albumRepository.SetCoverImage(albumId, mediaId);
            return Content("1");
        }

        /// <summary>
        /// Removes the media from album.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        public ActionResult RemoveMediaFromAlbum(int albumId, int mediaId)
        {
            _albumRepository.RemoveMediaFromAlbum(albumId, mediaId);
            return Content("1");
        }

        /// <summary>
        /// Adds the media to album.
        /// </summary>
        /// <param name="mediaId">The media id.</param>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        public ActionResult AddMediaToAlbum(int mediaId, int albumId)
        {
            _albumRepository.AddPhotoToAlbum(albumId, mediaId);
            return Content("1");
        }

        /// <summary>
        /// Gets the album view.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="cp">The cp.</param>
        /// <param name="text">The text.</param>
        /// <param name="retrieveMedia">The retrieve media.</param>
        /// <returns></returns>
	    private BySearchingView GetAlbumView(Album album, string cp, Func<List<Media>> retrieveMedia, string text = null)
        {
            BySearchingView albumView = ModelFactory<BySearchingView>(new {album, Pagination = _paginationService});

	        int index = (string.IsNullOrEmpty(cp) ? 1 : Convert.ToInt32(cp));
            List<Media> media = retrieveMedia();
            string queryText = (string.IsNullOrEmpty(text) ? string.Empty : "text=" + HttpUtility.HtmlEncode(text));
            albumView.Pagination.GeneratePaging(media, index, 20, "?" + queryText + "&cp={0}");
	        albumView.TotalCount = media.Count;

	        return albumView;
	    }

	    /// <summary>
		/// Gets the bread crumbs.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <returns></returns>
		private IDictionary<string, string> GetBreadCrumbs(Album album)
		{
			IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            breadCrumbs.Add(album.Name, UrlService.UserUrl("albums/#/show/" + album.AlbumId));
			breadCrumbs.Add("add", string.Empty);

			return breadCrumbs;
		}

        /// <summary>
        /// Add photos by the tags.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="text">The text.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
		public ActionResult Tags(Album album, string text, string cp)
		{
			IDictionary<string, string> breadCrumbs = GetBreadCrumbs(album);
			breadCrumbs.Add("by tags", string.Empty );

            List<Media> media = (string.IsNullOrEmpty(text)
                                     ? new List<Media>()
                                     : _mediaRepository.SearchTagsForPhotosThatDoNotIncludePhotosFromAlbumIdAndUserId(album.AlbumId, Owner.Id, new TagCollection(text)));

            Func<List<Media>> retrieveMedia = () => (media);

            BySearchingView albumView = GetAlbumView(album, cp, retrieveMedia, text);
		    albumView.PartialViewName = "ByTags";
            albumView.Set = _persistentCollectionService.Set(Authorization.Owner.Username + "AddPhotoTag" + album.AlbumId + text, media, Persistence.Temporary);
            string queryValues = (string.IsNullOrEmpty(text) ? string.Empty : "?text=" + HttpUtility.HtmlEncode(text).Trim() + "&cp=" + (string.IsNullOrEmpty(cp) ? 1 : Convert.ToInt32(cp)));
            _persistentCollectionService.SetBackUrl(Authorization.Owner.Username + "/albums/tags/" + album.AlbumId + queryValues, SiteCookie);

			return View("addphotos",albumView, breadCrumbs);
		}


        /// <summary>
        /// Shows all.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="text">The text.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
		public ActionResult SearchPhotos(Album album, string text, string cp)
		{
			IDictionary<string, string> breadCrumbs = GetBreadCrumbs(album);
			breadCrumbs.Add("by searching", string.Empty);

            List<Media> media = (string.IsNullOrEmpty(text)
                                     ? new List<Media>()
                                     : _mediaRepository.SearchPhotosThatDoNotIncludePhotosByAlbumId(album.AlbumId,
                                                                                                    Owner.Id,
                                                                                                    HttpUtility.HtmlEncode(text)));
            Func<List<Media>> retrieveMedia = () => (media);
            BySearchingView albumView = GetAlbumView(album, cp, retrieveMedia, text);
		    albumView.Text = HttpUtility.HtmlEncode(text);
		    albumView.PartialViewName = "BySearching";

            albumView.Set = _persistentCollectionService.Set(
                Authorization.Owner.Username + "AddphotosBySearching" + album.AlbumId + HttpUtility.HtmlEncode(text),
                media, Persistence.Permanent);

            string queryValues = (string.IsNullOrEmpty(text) ? string.Empty : "?text=" + HttpUtility.HtmlEncode(text).Trim() + "&cp=" + (string.IsNullOrEmpty(cp) ? 1 : Convert.ToInt32(cp)));
            _persistentCollectionService.SetBackUrl(Authorization.Owner.Username + "/albums/searchphotos/" + album.AlbumId + queryValues, SiteCookie);

            return View("addphotos",albumView, breadCrumbs);
		}

		/// <summary>
		/// Render this View
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(string id)
		{
			int? parentId = (string.IsNullOrEmpty(id) ? new int?() : Convert.ToInt32(id));
			IEnumerable<Album> breadcrumbTrail = GetBreadcrumbTrail(parentId);
			IDictionary<string, string> dictionary = GetCrumbs(breadcrumbTrail);

            List<Album> albums = (parentId.HasValue ? new List<Album> { _albumRepository.RetrieveByUserIdAndPrimayKey(Owner.Id, parentId.Value) } : _albumRepository.RetrieveTopLevelAlbumsByUserId(Owner.Id));
			List<AlbumLandingPageDecorator> landingPageDecorators = albums.ConvertAll(o =>
																						  {
																							  Media media = (o.Media != null ? o.Media.FirstOrDefault() : null);
																							  return new AlbumLandingPageDecorator(o) { RandomImage = GetRandomAlbumImage(media) };
																						  });

		    ManageAlbumsView manageAlbumsView = ModelFactory<ManageAlbumsView>(new {AlbumId = parentId, TopLevelAlbums = landingPageDecorators});
			return View(manageAlbumsView, dictionary);
		}
        
		/// <summary>
		/// Gets the breadcrumb trail.
		/// </summary>
		/// <param name="parentId">The parent id.</param>
		/// <returns></returns>
		private IEnumerable<Album> GetBreadcrumbTrail(int? parentId)
		{
			List<Album> breadcrumbTrail = new List<Album>();

			if (parentId.HasValue)
			{
                breadcrumbTrail = _albumRepository.RetrieveAlbumHierarchyByAlbumIdAndUserId(parentId.Value, Owner.Id);
			}

			return breadcrumbTrail;
		}

		/// <summary>
		/// Gets the crumbs.
		/// </summary>
		/// <param name="breadcrumbTrail">The breadcrumb trail.</param>
		/// <returns></returns>
		private IDictionary<string, string> GetCrumbs(IEnumerable<Album> breadcrumbTrail)
		{
			IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            IEnumerable<KeyValuePair<string, string>> pairs = breadcrumbTrail.Select(album => new KeyValuePair<string, string>(album.Name, UrlService.UserUrl("albums/" + album.AlbumId)));

			foreach (KeyValuePair<string, string> keyValuePair in pairs)
			{
				breadCrumbs.Add(keyValuePair);
			}

			return breadCrumbs;
		}
        
		/// <summary>
		/// Gets the album crumbs.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <returns></returns>
		private string GetAlbumCrumbs(Album album)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("/");
            List<Album> albumCrumb = _albumRepository.RetrieveAlbumHierarchyByAlbumIdAndUserId(album.AlbumId, Owner.Id);
			const string link = "<a href=\"{0}\" title=\" View '{1}'\" >{1}</a>";
			foreach (Album crumb in albumCrumb)
			{
                builder.AppendFormat("/ {0} ", string.Format(link, UrlService.UserUrl("albums/#/show/" + crumb.AlbumId), crumb.Name));
			}

			return builder.ToString();
		}


		/// <summary>
		/// Gets the random album image.
		/// </summary>
		/// <param name="media">The media.</param>
		/// <returns></returns>
		private string GetRandomAlbumImage(Media media)
		{
			string randomImageLink = UrlService.CreateRootUrl("content/images/nophotosfound.png");

			if (media != null)
			{
				string filePath = media.GetImageByPhotoType(PhotoType.Thumbnail).FilePath;

				if (!string.IsNullOrEmpty(filePath))
				{
                    randomImageLink = UrlService.CreateImageUrl(filePath);
				}

			}

			return randomImageLink;
		}

		/// <summary>
		/// Gets the bread crumbs.
		/// </summary>
		/// <returns></returns>
		public IDictionary<string, string> GetBreadCrumbs()
		{
			IDictionary<string, string> crumbs = new Dictionary<string, string>
													 {
														 {"home", UrlService.UserRoot()},
														 {"albums", UrlService.UserUrl("albums")}
													 };
			return crumbs;
		}

		/// <summary>
		/// Adds the album.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public ActionResult Add(Album album)
		{
		    AddAlbumView addAlbumView = ModelFactory<AddAlbumView>();

            if (album != null)
            {
                addAlbumView.AlbumCrumbs = GetAlbumCrumbs(album);
            }

		    return View(addAlbumView);
		}

	    /// <summary>
		/// Adds the album.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Add(AddAlbumView albumView, string id)
		{
			int albumId;
			int.TryParse(id, out albumId);

            Album album = new Album { Description = albumView.Description ?? string.Empty, Name = albumView.Name, UserId = Owner.Id };
	        albumView = SetAuthorizationAndUrlService(albumView);

			if (!string.IsNullOrEmpty(id))
			{
				album.ParentId = Convert.ToInt32(id);
			}

			_albumRepository.Save(album);
			albumView.UIMessage = album.Name + " added.";

			albumView.Name = string.Empty;
			albumView.Description = string.Empty;

			return View(albumView);
		}


		/// <summary>
		/// Removes the photos.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult RemovePhotos(Album album)
		{
			RemovePhotosFromAlbumView fromAlbumView = GetFromAlbumView(album);
			IDictionary<string, string> crumbs = GetRemovePhotoBreadcrumbs(album.AlbumId);
			return View(fromAlbumView, crumbs);
		}

		/// <summary>
		/// Gets from album view.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <returns></returns>
		private RemovePhotosFromAlbumView GetFromAlbumView(Album album)
		{
            List<Media> photos = _mediaRepository.RetrieveByAlbumIdAndUserId(Owner.Id, album.AlbumId);
		    RemovePhotosFromAlbumView removePhotos = ModelFactory<RemovePhotosFromAlbumView>(new {photos, album});
            return removePhotos;
		}

		/// <summary>
		/// Removes the photos.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <param name="ids">The ids.</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult RemovePhotos(Album album, int[] ids)
		{
			string message = string.Empty;

			if (ids != null)
			{
				_albumRepository.DeletePhotosFromAlbum(album.AlbumId, ids);
				message = string.Format("{0} photo(s) removed.", ids.Length);
			}

			RemovePhotosFromAlbumView fromAlbumView = GetFromAlbumView(album);
			IDictionary<string, string> crumbs = GetRemovePhotoBreadcrumbs(album.AlbumId);
			fromAlbumView.UIMessage = message;

			return View(fromAlbumView, crumbs);
		}

		/// <summary>
		/// Gets the remove photo breadcrumbs.
		/// </summary>
		/// <param name="albumId">The album id.</param>
		/// <returns></returns>
		public IDictionary<string, string> GetRemovePhotoBreadcrumbs(int albumId)
		{
			IDictionary<string, string> crumbs = GetHierarchicalBreadCrumbs(albumId);
            crumbs.Add("remove photos", UrlService.UserUrl(string.Format("albums/removephotos/{0}", albumId)));

			return crumbs;
		}

		/// <summary>
		/// Gets the hierarchical bread crumbs.
		/// </summary>
		/// <param name="albumId">The album id.</param>
		/// <returns></returns>
		private IDictionary<string, string> GetHierarchicalBreadCrumbs(int albumId)
		{
			IDictionary<string, string> breadCrumbs = GetBreadCrumbs();
            List<Album> albums = _albumRepository.RetrieveAlbumHierarchyByAlbumIdAndUserId(albumId, Owner.Id);
            List<KeyValuePair<string, string>> pairs = albums.ConvertAll(o => new KeyValuePair<string, string>(o.Name, UrlService.UserUrl(string.Format("albums/{0}", o.AlbumId))));

			foreach (KeyValuePair<string, string> keyValuePair in pairs)
			{
				breadCrumbs.Add(keyValuePair);
			}

			return breadCrumbs;
		}

		/// <summary>
		/// Albums the details.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <returns></returns>
		public JsonResult GetDetails(Album album)
		{

            List<Media> medias = _mediaRepository.RetrieveByAlbumIdAndUserId(Owner.Id, album.AlbumId);
            _persistentCollectionService.SetBackUrl(string.Format("{0}/albums/#/show/{1}", Authorization.Owner.Username, album.AlbumId), SiteCookie);
            string key = _persistentCollectionService.Set(Authorization.Owner.Username + "_albums_show_" + album.AlbumId, medias, Persistence.Permanent);

		    string adminLink = string.Empty;
            if (Authorization.IsOwner)
			{
				adminLink = GetAdminLink(album.AlbumId);
			}

            string photos = GenerateImageLinks(key,medias);

            var albumDetails = new { photos, title = album.Name, isAdmin = Authorization.IsOwner, description = album.Description, count = medias.Count, adminlinks = adminLink };
			return Json(albumDetails, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Gets the admin link.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		private string GetAdminLink(int id)
		{
			//0 - add album link
			//1 - edit album link
			//2 - remove album link
			//3 - add photos link
			//4 - remove photos link
			const string adminLinksFormat = @"<a class=""fanceyboxmodal"" href=""{0}"">add sub-album</a> | <a href=""{1}"">manage album</a> | <a class=""removealbum"" href=""{2}"" >delete album</a>";

            return string.Format(adminLinksFormat, 
                string.Format("{0}/albums/add/{1}", 
                UrlService.UserRoot(), id), 
                string.Format("{0}/albums/details/{1}", UrlService.UserRoot(), id), 
			   "javascript:");
		}

        /// <summary>
        /// Generates the image links.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="medias">The medias.</param>
        /// <returns></returns>
		private string GenerateImageLinks(string key, IEnumerable<Media> medias)
		{
			if (medias == null) throw new ArgumentNullException("medias");

			//0 - media id
			//1 - fullsize media
			//2 - title of media
			//3 - thumbnail image
			const string format = @"<li>
					 <span class=""image"">
						<a name=""{0}"" rel=""{1}"" href=""{4}"" class=""showimage"" title=""{2}""> 
							<img src=""{3}"" alt=""{2}"" /> 
						</a> 
					  </span> 
					</li>";

			StringBuilder builder = new StringBuilder();

			foreach (Media media in medias)
			{
                string fullsizePath = UrlService.CreateImageUrl(Owner.Username, media.GetImageByPhotoType(PhotoType.Websize).FilePath);
                string thumbnail = UrlService.CreateImageUrl(Owner.Username, media.GetImageByPhotoType(PhotoType.Thumbnail).FilePath);
			    string showLink = UrlService.UserUrl("photos/show/" + key + "/#/photo/" + media.MediaId);
                builder.AppendLine(string.Format(format, media.MediaId, fullsizePath, media.Title, thumbnail, showLink));
			}

			return builder.ToString();
		}


        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult GetNodes(string operation, int id)
        {
            int? parentId = (id == 0 ? new int?() : id);
            List<Album> albums = (parentId.HasValue ? _albumRepository.RetrieveAllByUserIdAndParentId(Owner.Id, parentId) : _albumRepository.RetrieveTopLevelAlbumsByUserId(Owner.Id));


             List<JsTreeNode> nodes = new List<JsTreeNode>();
            foreach (Album album in albums)
            {
                JsTreeNode node = new JsTreeNode
                                      {
                                          data = {title = album.Name + string.Format(" ({0})", album.PhotoCount)},
                                          attr= {id = "node_" + album.AlbumId},
                                          state = (album.ChildAlbumCount > 0 ? "closed" : string.Empty)
                                      };
                nodes.Add(node);
            }

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

		/// <summary>
		/// Nodeses the specified root.
		/// </summary>
		/// <param name="root">The root.</param>
		/// <returns></returns>
		public JsonResult Nodes(string root)
		{
			int? parentId = (root == "source" ? new int?() : Convert.ToInt32(root));
            List<Album> albums = (parentId.HasValue ? _albumRepository.RetrieveAllByUserIdAndParentId(Owner.Id, parentId) : _albumRepository.RetrieveTopLevelAlbumsByUserId(Owner.Id));

			List<Node> nodes = albums.Select(album => new Node
														  {
															  text = album.Name + string.Format(" ({0})", album.PhotoCount),
															  hasChildren = album.ChildAlbumCount > 0,
															  id = album.AlbumId
														  }).ToList();

			return Json(nodes, JsonRequestBehavior.AllowGet);
		}

		private class Node
		{
			// ReSharper disable InconsistentNaming
			// ReSharper disable UnusedAutoPropertyAccessor.Local
			public string text { get; set; }
			// ReSharper restore UnusedAutoPropertyAccessor.Local

			// ReSharper disable UnusedMember.Local
			public List<Node> children { get; set; }
			// ReSharper restore UnusedMember.Local

			// ReSharper disable UnusedAutoPropertyAccessor.Local
			public bool hasChildren { get; set; }

			public int id { get; set; }
			// ReSharper restore UnusedAutoPropertyAccessor.Local
			// ReSharper restore InconsistentNaming
		}
	}
}
