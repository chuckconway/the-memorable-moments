using AutoMapper;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.UI.Models.Views;

namespace TheMemorableMoments.UI.Web.AutoMapper.Profiles
{
    public class UserToSettingsProfile : Profile
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<UserSettings, SettingsView>();
        }
    }
}