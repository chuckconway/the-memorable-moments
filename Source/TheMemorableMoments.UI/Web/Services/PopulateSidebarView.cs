using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views;

namespace TheMemorableMoments.UI.Web.Services
{
    public class PopulateSidebarView : IPopulateSidebarView
    {
        private readonly IRecentRepository _recentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IFriendRepository _friendRepository;

        public PopulateSidebarView(IRecentRepository recentRepository, 
                                   ITagRepository tagRepository, 
                                   IAlbumRepository albumRepository, 
                                   IFriendRepository friendRepository)
        {
            _recentRepository = recentRepository;
            _friendRepository = friendRepository;
            _albumRepository = albumRepository;
            _tagRepository = tagRepository;
        }

        public PopulateSidebarView():this(DependencyInjection.Resolve<IRecentRepository>(),
                                          DependencyInjection.Resolve<ITagRepository>(),
                                          DependencyInjection.Resolve<IAlbumRepository>(),
                                          DependencyInjection.Resolve<IFriendRepository>()) { }

        public void PopulateView(ISidebarView view, User user)
        {
            view.SidebarTags = _tagRepository.RetrieveTagsByUserId(user.Id);
            view.SidebarAlbums = _albumRepository.RetrieveTopLevelAlbumsByUserId(user.Id);
            view.SidebarFriends = _friendRepository.RetrieveFriendsByUserId(user.Id);
            view.SidebarRecentActivity = _recentRepository.RetrieveLast30Days(user.Id);
            view.SidebarYearWithCount = _recentRepository.RetrieveYearsForUser(user.Id);
            view.SidebarRecentUploads = _recentRepository.RetrieveRecentUploads(user.Id, 6);
        }
    }
}