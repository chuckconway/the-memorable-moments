using AutoMapper;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views.Photos;

namespace TheMemorableMoments.UI.Web.AutoMapper.Profiles
{
    public class SavedPhotoModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SavedPhotoView, Media>()
                .ForMember(m => m.Month, s => s.MapFrom(src => src.SelectedMonth))
                .ForMember(d => d.Day, s => s.MapFrom(src => src.SelectedDay))
                .ForMember(s => s.Description, s => s.MapFrom(src => src.Story))
                .ForMember(s => s.Status, s => s.MapFrom(src => src.MediaStatus.ParseEnum<MediaStatus>()));
        }
    }
}