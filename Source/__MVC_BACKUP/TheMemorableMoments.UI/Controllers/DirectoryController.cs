using System.Collections.Generic;
using System.Web.Mvc;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views;
using TheMemorableMoments.UI.Web.Services;

namespace TheMemorableMoments.UI.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryController"/> class.
        /// </summary>
        /// <param name="memberRepository">The member repository.</param>
        public DirectoryController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Member> members = _memberRepository.RetrieveMembers();
            return View(new DirectoryView { Members = members, UrlService = UserUrlService.GetInstance()});
        }

    }
}
