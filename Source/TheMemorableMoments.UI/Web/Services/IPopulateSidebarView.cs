using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views;

namespace TheMemorableMoments.UI.Web.Services
{
    public interface IPopulateSidebarView
    {
        void PopulateView(ISidebarView view, User user);
    }
}