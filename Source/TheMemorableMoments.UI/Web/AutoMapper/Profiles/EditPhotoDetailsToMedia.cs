using AutoMapper;
using Chucksoft.Core.Extensions;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.UI.Models.Views.Photos;

namespace TheMemorableMoments.UI.Web.AutoMapper.Profiles
{
    public class EditPhotoDetailsToMedia : Profile
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<EditPhotoDetails, Media>()
                .ForMember(m => m.Month, s => s.MapFrom(src => src.SelectedMonth))
                .ForMember(d => d.Day, s => s.MapFrom(src => src.SelectedDay))
                .ForMember(d => d.Tags, s => s.MapFrom(src => (string.IsNullOrEmpty(src.Tags) ? "untagged" : src.Tags)))
                .ForMember(d => d.Year, s => s.MapFrom(src => src.SelectedYear))
                .ForMember(d => d.Status, s => s.MapFrom(src => src.MediaStatus.ParseEnum<MediaStatus>()))
                .ForMember(d => d.Description, s => s.MapFrom(src => src.Story ?? string.Empty))
                .ForMember(d => d.Title, s => s.MapFrom(src => src.Title ?? string.Empty))
                .ForMember(d => d.Location, s => s.MapFrom(src => new Location
                                                                      {
                                                                          Latitude = src.Latitude.GetValueOrDefault(),
                                                                          LocationName = src.LocationName,
                                                                          Longitude = src.Longitude.GetValueOrDefault(),
                                                                          MapTypeId = src.MapTypeId,
                                                                          Zoom = src.Zoom.GetValueOrDefault()
                                                                      }));
        }
    }
}