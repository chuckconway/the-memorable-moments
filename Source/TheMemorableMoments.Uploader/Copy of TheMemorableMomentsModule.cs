using Chucksoft.Core.Services;
using Ninject.Modules;
using TheMemorableMoments.Domain.Services;
using TheMemorableMoments.FileService;
using TheMemorableMoments.Infrastructure.Repositories;
using TheMemorableMoments.Infrastructure.Repositories.Services;
using TheMemorableMoments.Infrastructure.Repositories.Uploader;

namespace TheMemorableMoments.Uploader
{
    public class TheMemorableMomentsModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ITagRepository>().To<TagRepository>();
            Bind<IAlbumRepository>().To<AlbumRepository>();
            Bind<IMediaQueueRepository>().To<MediaQueueRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IResizerService>().To<ResizerService>();
            Bind<IConfigurationRepository>().To<ConfigurationRepository>();
            Bind<IMediaFileService>().To<MediaFileService>();
            Bind<IMediaRepository>().To<MediaRepository>();
            Bind<IMediaFileRepository>().To<MediaFileRepository>();
            Bind<ICryptographyService>().To<CryptographyService>();
            Bind<IMemberRepository>().To<MemberRepository>();

            Bind<ICommentRepository>().To<CommentRepository>();
            Bind<IFriendRepository>().To<FriendRepository>();
            Bind<IRecentRepository>().To<RecentRepository>();

            Bind<IMediaFileHydrationService>().To<MediaFileHydrationService>();



            Bind<IUploaderMediaRepository>().To<UploaderMediaRepository>();
            Bind<IImageRotateService>().To<ImageRotateService>();
            Bind<IConfigurationService>().To<ConfigurationService>();
            Bind<IQueueFileService>().To<QueueFileService>();
            Bind<IPagingRepository>().To<PagingRepository>();
            Bind<IPagingService>().To<PagingService>();
            Bind<ITrackMediaViewsService>().To<TrackMediaViewsService>();
            Bind<IMediaViewsRepository>().To<MediaViewsRepository>();
            Bind<IInvitationRepository>().To<InvitationRepository>();

            Bind<IUpdateTagsService>().To<UpdateTagsService>();
            Bind<IWaitingListRepository>().To<WaitingListRepository>();

            Bind<IReportingRepository>().To<ReportingRepository>();

            Bind<IJoinRepository>().To<JoinRepository>();

            Bind<IMediaFileService>().To<MediaFileService>();
            Bind<IMediaFilenameService>().To<MediaFilenameService>();
            Bind<IFileService>().To<AzureFileService>();
        }
    }
}