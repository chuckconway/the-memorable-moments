using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chucksoft.Core;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models;
using TheMemorableMoments.UI.Models.Views.Comments;
using TheMemorableMoments.UI.Web.Controllers;

namespace TheMemorableMoments.UI.Controllers.User
{
    public class CommentsController : HybridController
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IPaginationService<MediaComments> _paginationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="mediaRepository">The media repository.</param>
        /// <param name="paginationService">The pagination service.</param>
        public CommentsController(ICommentRepository commentRepository, IMediaRepository mediaRepository, IPaginationService<MediaComments> paginationService)
        {
            _commentRepository = commentRepository;
            _paginationService = paginationService;
            _mediaRepository = mediaRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Index(string cp)
        {
            return IndexGet(cp);
        }

        /// <summary>
        /// Alls the specified cp.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult All(string cp)
        {
            return IndexGet(cp);
        }

        /// <summary>
        /// Indexes the get.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        private ActionResult IndexGet(string cp)
        {
            ManageCommentsView view = GetManageCommentView(() => _commentRepository.RetrieveCommentsByUserId(Owner.Id), cp, "all");
            IDictionary<string, string> crumbs = GetBreadCrumbs("all");
            return View("Index", view, crumbs);
        }

        /// <summary>
        /// Indexes the specified comment action.
        /// </summary>
        /// <param name="commentAction">The comment action.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Index(CommentAction commentAction, string cp)
        {
            return IndexPost(commentAction, cp);
        }

        /// <summary>
        /// Alls the specified comment action.
        /// </summary>
        /// <param name="commentAction">The comment action.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult All(CommentAction commentAction, string cp)
        {
            return IndexPost(commentAction, cp);
        }

        /// <summary>
        /// Indexes the post.
        /// </summary>
        /// <param name="commentAction">The comment action.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        private ActionResult IndexPost(CommentAction commentAction, string cp)
        {
            IDictionary<string, string> crumbs = GetBreadCrumbs("all");
            ManageCommentsView view = GeCommentView(commentAction, () => _commentRepository.RetrieveCommentsByUserId(Owner.Id), cp, "all");
// ReSharper disable Asp.NotResolved
            return View("Index", view, crumbs);
// ReSharper restore Asp.NotResolved
        }

        /// <summary>
        /// Gets the manage comment view.
        /// </summary>
        /// <param name="getCommentsMethod">The get comments method.</param>
        /// <param name="cp">The cp.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        private ManageCommentsView GetManageCommentView(Func<List<Comment>> getCommentsMethod, string cp, string view)
        {
            List<Comment> comments = getCommentsMethod();  //
            ManageCommentsView commentView = GetCommentView(comments, cp, view);
            return commentView;
        }

        /// <summary>
        /// Ges the commentt view.
        /// </summary>
        /// <param name="commentAction">The comment action.</param>
        /// <param name="getCommentsMethod">The get comments method.</param>
        /// <param name="cp">The cp.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        private ManageCommentsView GeCommentView(CommentAction commentAction, Func<List<Comment>> getCommentsMethod, string cp, string view)
        {
            string message;
            if(commentAction.CommentActionType != "-1")
            {
                if (commentAction.CommentId != null && commentAction.CommentId.Length > 0)
                {
                    CommentStatus commentStatus = commentAction.CommentActionType.ParseEnum<CommentStatus>();
                    _commentRepository.UpdateCommentStatus(commentStatus, commentAction.CommentId, Owner.Id);
                    message = string.Format("Selected comments have been changed to {0}.", commentAction.CommentActionType);
                }
                else
                {
                    message = "Please select one or more comments.";
                }
            }
            else
            {
                message = "Please select an action.";
            }
            
            ManageCommentsView commentView = GetManageCommentView(getCommentsMethod, cp, view);
            commentView.UIMessage = message;
            return commentView;
        }


        /// <summary>
        /// Sets the bread crumb.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public IDictionary<string, string> CommentCrumb(Media media, int photoId)
        {
            IDictionary<string, string> crumbs = SetBreadCrumb(new KeyValuePair<string, string>((string.IsNullOrEmpty(media.Title) ? "Untitled" : media.Title), UrlService.UserUrl(string.Format("photos/show/#/photo/{0}", photoId))));
            crumbs.Add("Comments", UrlService.UserUrl(string.Format("comments/leave/{0}", photoId)));

            return crumbs;
        }

        /// <summary>
        /// Sets the bread crumb.
        /// </summary>
        /// <param name="currentView">The current view.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public IDictionary<string, string> SetBreadCrumb(string currentView, string text)
        {
            return SetBreadCrumb(new KeyValuePair<string, string>(text, UrlService.UserUrl(string.Format("photos/{0}", currentView))));
        }

        /// <summary>
        /// Sets the bread crumb.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IDictionary<string, string> SetBreadCrumb(KeyValuePair<string, string> value)
        {
// ReSharper disable UseObjectOrCollectionInitializer
            IDictionary<string, string> crumbs = new Dictionary<string, string>
// ReSharper restore UseObjectOrCollectionInitializer
                                                     {
                                                         {"home", UrlService.UserRoot()},
                                                         {"photos", string.Empty}
                                                     };
            crumbs.Add(value);
            return crumbs;
        }

        /// <summary>
        /// Commentses the crumbs.
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> CommentsCrumbs()
        {
            IDictionary<string, string> crumbs = new Dictionary<string, string>
                                                     {
                                                         {"home", UrlService.UserRoot()},
                                                         {"photos",string.Empty},
                                                         {"comments", UrlService.UserUrl("comments")}
                                                     };
            return crumbs;
        }

        /// <summary>
        /// Gets the comment view.
        /// </summary>
        /// <param name="comments">The comments.</param>
        /// <param name="cp">The cp.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        private ManageCommentsView GetCommentView(IEnumerable<Comment> comments, string cp, string view)
        {
            List<StatusCount<CommentStatus>> commentStatuses = _commentRepository.RetrieveCommentStatusCountByUserId(Owner.Id);
            return ModelFactory<ManageCommentsView>(new
                                                        {
                                                            Pagination = GetCommentMedia(comments, cp),
                                                            CommentStatuses = commentStatuses,
                                                            view,
                                                            MediaStatusCount = _mediaRepository.GetMediaStatusCountByUserId(Owner.Id)
                                                        });
        }


        /// <summary>
        /// Spams this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Spam(string cp)
        {
            List<Comment> comments = _commentRepository.RetrieveCommentByStatusAndUserId(Owner.Id, CommentStatus.Spam);
            const string breadCrumbName = "spam";
            IDictionary<string, string> crumbs = GetBreadCrumbs(breadCrumbName);
            ManageCommentsView view = GetCommentView(comments, cp, breadCrumbName);

            return View("Index", view, crumbs);
        }


        /// <summary>
        /// Spams this instance.
        /// </summary>
        /// <param name="commentAction">The comment action.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Spam(CommentAction commentAction, string cp)
        {  
            const string breadCrumbName = "spam";
            ManageCommentsView view = GetCommentView(_commentRepository.RetrieveCommentByStatusAndUserId(Owner.Id, CommentStatus.Spam), cp, breadCrumbName);
            IDictionary<string, string> crumbs = GetBreadCrumbs(breadCrumbName);

            return View("Index", view, crumbs);
        }

        /// <summary>
        /// Gets the bread crumbs.
        /// </summary>
        /// <param name="breadCrumbName">Name of the bread crumb.</param>
        /// <returns></returns>
        private IDictionary<string, string> GetBreadCrumbs(string breadCrumbName)
        {
            IDictionary<string, string> commentBreadcrumbs = CommentsCrumbs();
            commentBreadcrumbs.Add(breadCrumbName, UrlService.UserUrl(string.Format("comments/{0}", breadCrumbName)));
            return commentBreadcrumbs;
        }

        /// <summary>
        /// Pendings this instance.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Pending(string cp)
        {
            List<Comment> comments = _commentRepository.RetrieveCommentByStatusAndUserId(Owner.Id, CommentStatus.Pending);
            const string breadCrumbName = "pending";

            IDictionary<string, string> crumbs = GetBreadCrumbs(breadCrumbName);
            ManageCommentsView view = GetCommentView(comments, cp, breadCrumbName);

            return View("Index", view, crumbs);
        }

        /// <summary>
        /// Spams this instance.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Pending(CommentAction commentAction, string cp)
        {
            const string breadCrumbName = "pending";
            ManageCommentsView view = GetCommentView(_commentRepository.RetrieveCommentByStatusAndUserId(Owner.Id, CommentStatus.Pending), cp, breadCrumbName);

            IDictionary<string, string> crumbs = GetBreadCrumbs(breadCrumbName);
            return View("Index", view, crumbs);
        }

        /// <summary>
        /// Approveds this instance.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Approved(string cp)
        {
            List<Comment> comments = _commentRepository.RetrieveCommentByStatusAndUserId(Owner.Id, CommentStatus.Approved);
            const string breadCrumbName = "approved";

            IDictionary<string, string> crumbs = GetBreadCrumbs(breadCrumbName);
            ManageCommentsView view = GetCommentView(comments, cp, breadCrumbName);

            return View("Index", view, crumbs);
        }

        /// <summary>
        /// Spams this instance.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Approved(CommentAction commentAction, string cp)
        {
            const string approved = "approved";
            ManageCommentsView view = GetCommentView(_commentRepository.RetrieveCommentByStatusAndUserId(Owner.Id, CommentStatus.Approved), cp, approved);
            IDictionary<string, string> crumbs = GetBreadCrumbs(approved);

            return View("Index", view, crumbs);
        }

        /// <summary>
        /// Commentses the specified username.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Leave(Media media)
        {
            List<Comment> comments = _commentRepository.RetrieveAllByMediaId(media.MediaId);
            IDictionary<string, string> crumb = CommentCrumb(media, media.MediaId);

            CommentsView commentsView = ModelFactory<CommentsView>(new { media, comments });
            return View(commentsView, crumb);
        }

        /// <summary>
        /// Commentses the specified username.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="saveView">The save view.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Leave(string id, CommentSaveView saveView)
        {
            int photoId = Convert.ToInt32(id);
            Media media = _mediaRepository.RetrieveByPrimaryKeyAndUserId(photoId, Owner.Id);
            Comment comment = GetComment(Owner, saveView, media);
            _commentRepository.Save(comment);

            IDictionary<string, string> crumb = CommentCrumb(media, photoId);
            List<Comment> comments = _commentRepository.RetrieveAllByMediaId(media.MediaId);

            CommentsView commentsView = ModelFactory<CommentsView>(new { media, comments });
            commentsView.UIMessage = "Thank you for commenting.";

            return View(commentsView, crumb);
        }


        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="saveView">The save view.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        private Comment GetComment(IId<int> user, CommentSaveView saveView, IMediaFiles media)
        {
            return new Comment
            {
                Text = saveView.Comments,
                MediaId = media.MediaId,
                CommentStatus = CommentStatus.Approved,
                Email = saveView.Email,
                Name = saveView.Name,
                SiteUrl = saveView.Website ?? string.Empty,
                CommentDate = DateTime.Now,
                Ip = HttpContext.Request.UserHostAddress,
                UserAgent = HttpContext.Request.UserAgent,
                UserId = user.Id
            };
        }

        /// <summary>
        /// Gets the comment media.
        /// </summary>
        /// <param name="comments">The comments.</param>
        /// <param name="cp">The cp.</param>
        /// <returns></returns>
        private IPaginationService<MediaComments> GetCommentMedia(IEnumerable<Comment> comments, string cp)
        {
            List<MediaComments> mediaComments = (from comment in comments
                                                 let m = _mediaRepository.RetrieveByPrimaryKeyAndUserId(comment.MediaId, Owner.Id)
                                                 select new MediaComments(m, comment)).ToList();

            int currentPage = (string.IsNullOrEmpty(cp) ? 1 : Convert.ToInt32(cp));
            _paginationService.GeneratePaging(mediaComments, currentPage, 20, "?cp={0}");

            return _paginationService;
        }

    }
}