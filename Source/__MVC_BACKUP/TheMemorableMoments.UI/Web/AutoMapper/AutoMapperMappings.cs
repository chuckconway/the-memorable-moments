using AutoMapper;
using TheMemorableMoments.UI.Web.AutoMapper.Profiles;

namespace TheMemorableMoments.UI.Web.AutoMapper
{
    public static class AutoMapperMappings
    {
        public static void Initialize()
        {
            Mapper.Initialize(c =>
                                  {
                                      c.AddProfile(new SavedPhotoModelProfile());
                                      c.AddProfile(new UserToSettingsProfile());
                                      c.AddProfile(new UploadPhotoDetailsToUploadBatch());
                                      c.AddProfile(new EditPhotoDetailsToMedia());
                                  });
        }

    }
}