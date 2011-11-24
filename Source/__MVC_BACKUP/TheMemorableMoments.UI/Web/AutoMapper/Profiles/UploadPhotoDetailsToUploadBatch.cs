using System;
using AutoMapper;
using TheMemorableMoments.Domain.Model.Uploader;
using TheMemorableMoments.UI.Models.Views.Upload;

namespace TheMemorableMoments.UI.Web.AutoMapper.Profiles
{
    public class UploadPhotoDetailsToUploadBatch : Profile
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<UploadPhotoDetails, UploadBatch>()
            .ForMember(m => m.LocationName, s => s.MapFrom(src => src.LocationName ?? string.Empty))
            .ForMember(m => m.Albums, s => s.MapFrom(src => src.SelectedAlbums))
            .ForMember(m => m.BatchId, s => s.MapFrom(src => new Guid(src.BatchId)))
            .ForMember(m => m.Month, s => s.MapFrom(src => src.SelectedMonth))
            .ForMember(d => d.Day, s => s.MapFrom(src => src.SelectedDay))
            .ForMember(d => d.Tags, s => s.MapFrom(src => (string.IsNullOrEmpty(src.Tags) ? "untagged" : src.Tags)))
            .ForMember(d => d.Year, s => s.MapFrom(src => src.SelectedYear));
        }
    }
}