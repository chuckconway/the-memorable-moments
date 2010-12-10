using Autofac;
using Chucksoft.Core.Drawing;
using Chucksoft.Core.Services;
using Chucksoft.Storage;
using TheMemorableMoments.Domain.Services;

using TheMemorableMoments.Infrastructure.Repositories;
using TheMemorableMoments.Infrastructure.Repositories.Services;
using TheMemorableMoments.Infrastructure.Repositories.Uploader;

namespace TheMemorableMoments.Uploader
{

    public class TheMemorableMomentsContainerBuilder : ContainerBuilder
    {
        /// <summary>
        /// Loads the module inUse the kernel.
        /// </summary>
        public TheMemorableMomentsContainerBuilder()
        {
            this.RegisterType<TagRepository>().As<ITagRepository>();
            this.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            this.RegisterType<MediaQueueRepository>().As<IMediaQueueRepository>();
            this.RegisterType<UserRepository>().As<IUserRepository>();
            this.RegisterType<ResizerService>().As<IResizerService>();
            this.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>();
            this.RegisterType<MediaFileService>().As<IMediaFileService>();
            this.RegisterType<MediaRepository>().As<IMediaRepository>();
            this.RegisterType<MediaFileRepository>().As<IMediaFileRepository>();
            this.RegisterType<CryptographyService>().As<ICryptographyService>();
            this.RegisterType<MemberRepository>().As<IMemberRepository>();

            this.RegisterType<CommentRepository>().As<ICommentRepository>();
            this.RegisterType<FriendRepository>().As<IFriendRepository>();
            this.RegisterType<RecentRepository>().As<IRecentRepository>();

            this.RegisterType<MediaFileHydrationService>().As<IMediaFileHydrationService>();

            this.RegisterType<UploaderMediaRepository>().As<IUploaderMediaRepository>();
            this.RegisterType<ImageService>().As<IImageService>();
            this.RegisterType<ConfigurationService>().As<IConfigurationService>();

            this.RegisterType<QueueFileService>().As<IQueueFileService>();
            this.RegisterType<PagingRepository>().As<IPagingRepository>();
            this.RegisterType<PagingService>().As<IPagingService>();
            this.RegisterType<TrackMediaViewsService>().As<ITrackMediaViewsService>();
            this.RegisterType<MediaViewsRepository>().As<IMediaViewsRepository>();
            this.RegisterType<InvitationRepository>().As<IInvitationRepository>();

            this.RegisterType<UpdateTagsService>().As<IUpdateTagsService>();
            this.RegisterType<WaitingListRepository>().As<IWaitingListRepository>();

            this.RegisterType<ReportingRepository>().As<IReportingRepository>();

            this.RegisterType<JoinRepository>().As<IJoinRepository>();
            this.RegisterType<MediaFileService>().As<IMediaFileService>();
            this.RegisterType<MediaFilenameService>().As<IMediaFilenameService>();
            this.RegisterType<AzureStorage>().As<IStorage>();


            this.RegisterType<LocationRepository>().As<ILocationRepository>();
            this.RegisterType<ImageConverter>().As<IImageConverter>();
            this.RegisterType<MediaBatchService>().As<IMediaBatchService>();

            this.RegisterType<LocationRepository>().As<ILocationRepository>();
            this.RegisterType<BelongsToAlbumRepository>().As<IBelongsToAlbumRepository>();



        }
    }
}
