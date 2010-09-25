/*
Deployment script for TheMemorableMoments.Database
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, NUMERIC_ROUNDABORT, QUOTED_IDENTIFIER OFF;


GO
:setvar DatabaseName "TheMemorableMoments.Database"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [ThePhotoProject], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\ThePhotoProject.mdf', SIZE = 13312 KB, FILEGROWTH = 1024 KB)
    LOG ON (NAME = [ThePhotoProject_log], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\ThePhotoProject_log.ldf', SIZE = 7616 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %) COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS OFF,
                ANSI_PADDING OFF,
                ANSI_WARNINGS OFF,
                ARITHABORT OFF,
                CONCAT_NULL_YIELDS_NULL OFF,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER OFF,
                ANSI_NULL_DEFAULT OFF,
                CURSOR_DEFAULT GLOBAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY CHECKSUM,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[Album]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Album] (
    [AlbumId]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50)   NOT NULL,
    [Description]      NVARCHAR (4000) NULL,
    [UserId]           INT             NOT NULL,
    [ParentId]         INT             NULL,
    [CreateDate]       DATETIME2 (7)   NOT NULL,
    [LastModifiedDate] DATETIME2 (7)   NOT NULL,
    [CoverMediaId]     INT             NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Album...';


GO
ALTER TABLE [dbo].[Album]
    ADD CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED ([AlbumId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[AlbumMedia]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[AlbumMedia] (
    [AlbumId]    INT           NOT NULL,
    [MediaId]    INT           NOT NULL,
    [CreateDate] DATETIME2 (7) NOT NULL,
    [Position]   INT           NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_AlbumMedia_1...';


GO
ALTER TABLE [dbo].[AlbumMedia]
    ADD CONSTRAINT [PK_AlbumMedia_1] PRIMARY KEY CLUSTERED ([AlbumId] ASC, [MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Comment]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Comment] (
    [CommentId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NULL,
    [Email]         NVARCHAR (255) NULL,
    [SiteUrl]       NVARCHAR (100) NULL,
    [Ip]            NVARCHAR (100) NOT NULL,
    [UserAgent]     NVARCHAR (500) NOT NULL,
    [CommentStatus] NVARCHAR (50)  NOT NULL,
    [Text]          NVARCHAR (MAX) NOT NULL,
    [CommentDate]   DATETIME       NOT NULL,
    [UserId]        INT            NULL,
    [ParentId]      INT            NULL,
    [MediaId]       INT            NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Comment...';


GO
ALTER TABLE [dbo].[Comment]
    ADD CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([CommentId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Configuration]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Configuration] (
    [ConfigurationId] UNIQUEIDENTIFIER NOT NULL,
    [SiteName]        NVARCHAR (150)   NOT NULL,
    [SiteTagLine]     NVARCHAR (150)   NULL,
    [Membership]      NVARCHAR (50)    NOT NULL,
    [ThumbNailHeight] SMALLINT         NOT NULL,
    [ThumbNailWidth]  SMALLINT         NOT NULL,
    [WebHeight]       SMALLINT         NOT NULL,
    [WebWidth]        SMALLINT         NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Exif]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Exif] (
    [MediaId] INT            NOT NULL,
    [Key]     NVARCHAR (50)  NOT NULL,
    [Value]   NVARCHAR (250) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Exif_1...';


GO
ALTER TABLE [dbo].[Exif]
    ADD CONSTRAINT [PK_Exif_1] PRIMARY KEY CLUSTERED ([MediaId] ASC, [Key] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[File]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[File] (
    [FileId]           INT            IDENTITY (1, 1) NOT NULL,
    [FilePath]         NVARCHAR (255) NOT NULL,
    [FileExtension]    NVARCHAR (10)  NOT NULL,
    [OriginalFileName] NVARCHAR (200) NOT NULL,
    [CreateDate]       DATETIME2 (7)  NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_File...';


GO
ALTER TABLE [dbo].[File]
    ADD CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([FileId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Friend]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Friend] (
    [FriendId]   INT           NOT NULL,
    [UserId]     INT           NOT NULL,
    [CreateDate] DATETIME2 (7) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Invitation]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Invitation] (
    [InvitationId] INT            IDENTITY (1, 1) NOT NULL,
    [Email]        NVARCHAR (250) NOT NULL,
    [UserId]       INT            NOT NULL,
    [CreateDate]   DATETIME2 (7)  NOT NULL,
    [Sent]         BIT            NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Location]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Location] (
    [LocationId]   INT              IDENTITY (1, 1) NOT NULL,
    [LocationName] NVARCHAR (250)   NOT NULL,
    [Latitude]     NUMERIC (26, 13) NOT NULL,
    [Longitude]    NUMERIC (26, 13) NOT NULL,
    [UserId]       INT              NOT NULL,
    [Zoom]         TINYINT          NOT NULL,
    [MapTypeId]    NVARCHAR (50)    NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Location...';


GO
ALTER TABLE [dbo].[Location]
    ADD CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Media]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Media] (
    [MediaId]      INT           IDENTITY (1, 1) NOT NULL,
    [UserId]       INT           NOT NULL,
    [Status]       NVARCHAR (50) NOT NULL,
    [CreateDate]   DATETIME2 (7) NOT NULL,
    [UploadStatus] NVARCHAR (50) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Photo...';


GO
ALTER TABLE [dbo].[Media]
    ADD CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED ([MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[MediaFile]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[MediaFile] (
    [MediaId]     INT           NOT NULL,
    [FileId]      INT           NOT NULL,
    [MediaFormat] NVARCHAR (50) NOT NULL,
    [MediaType]   NVARCHAR (50) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_PhotoFile...';


GO
ALTER TABLE [dbo].[MediaFile]
    ADD CONSTRAINT [PK_PhotoFile] PRIMARY KEY CLUSTERED ([MediaId] ASC, [FileId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[MediaLedger]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[MediaLedger] (
    [MediaLedgerId] INT             IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (100)  NULL,
    [Description]   NVARCHAR (2000) NULL,
    [Tags]          NVARCHAR (250)  NULL,
    [MediaYear]     INT             NULL,
    [MediaMonth]    INT             NULL,
    [MediaDay]      INT             NULL,
    [MediaId]       INT             NOT NULL,
    [InsertDate]    DATETIME2 (7)   NOT NULL,
    [UserId]        INT             NOT NULL,
    [LocationId]    INT             NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_MediaLedger...';


GO
ALTER TABLE [dbo].[MediaLedger]
    ADD CONSTRAINT [PK_MediaLedger] PRIMARY KEY CLUSTERED ([MediaLedgerId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[MediaQueue]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[MediaQueue] (
    [MediaQueueId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [MediaId]      INT             NOT NULL,
    [MediaBytes]   VARBINARY (MAX) NOT NULL,
    [Filename]     NVARCHAR (250)  NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_MediaQueue...';


GO
ALTER TABLE [dbo].[MediaQueue]
    ADD CONSTRAINT [PK_MediaQueue] PRIMARY KEY CLUSTERED ([MediaQueueId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[MediaUploadBatch]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[MediaUploadBatch] (
    [UploadBatch] UNIQUEIDENTIFIER NOT NULL,
    [MediaId]     INT              NOT NULL,
    [CreateDate]  DATETIME2 (7)    NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_MediaUploadBatch...';


GO
ALTER TABLE [dbo].[MediaUploadBatch]
    ADD CONSTRAINT [PK_MediaUploadBatch] PRIMARY KEY CLUSTERED ([UploadBatch] ASC, [MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[MediaViewed]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[MediaViewed] (
    [MediaViewedId] INT           IDENTITY (1, 1) NOT NULL,
    [UserId]        INT           NOT NULL,
    [MediaId]       INT           NOT NULL,
    [LastViewed]    DATETIME2 (7) NOT NULL,
    [ViewCount]     INT           NOT NULL,
    [FirstViewed]   DATETIME2 (7) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[RecentActivity]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[RecentActivity] (
    [RecentActivityId] INT           IDENTITY (1, 1) NOT NULL,
    [ActivityType]     NVARCHAR (50) NOT NULL,
    [ActivityCount]    INT           NOT NULL,
    [UserId]           INT           NOT NULL,
    [CreateDate]       DATETIME2 (7) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_RecentActivity...';


GO
ALTER TABLE [dbo].[RecentActivity]
    ADD CONSTRAINT [PK_RecentActivity] PRIMARY KEY CLUSTERED ([RecentActivityId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Tag]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Tag] (
    [TagId]       INT             IDENTITY (1, 1) NOT NULL,
    [TagName]     NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (4000) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Tag...';


GO
ALTER TABLE [dbo].[Tag]
    ADD CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([TagId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[TagMedia]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[TagMedia] (
    [TagId]      INT           NOT NULL,
    [MediaId]    INT           NOT NULL,
    [CreateDate] DATETIME2 (7) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_TagPhoto...';


GO
ALTER TABLE [dbo].[TagMedia]
    ADD CONSTRAINT [PK_TagPhoto] PRIMARY KEY CLUSTERED ([TagId] ASC, [MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Token]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Token] (
    [Token]      NVARCHAR (50) NOT NULL,
    [CreateDate] DATETIME2 (7) NOT NULL,
    [IsValid]    BIT           NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[UploadBatch]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[UploadBatch] (
    [BatchId]      UNIQUEIDENTIFIER NOT NULL,
    [LocationName] NVARCHAR (100)   NULL,
    [Latitude]     DECIMAL (18, 12) NULL,
    [Longitude]    DECIMAL (18, 12) NULL,
    [Zoom]         INT              NULL,
    [MapTypeId]    NVARCHAR (50)    NULL,
    [Year]         INT              NULL,
    [Month]        INT              NULL,
    [Day]          INT              NULL,
    [Albums]       NVARCHAR (150)   NULL,
    [Tags]         NVARCHAR (500)   NULL,
    [MediaStatus]  NVARCHAR (50)    NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Batch...';


GO
ALTER TABLE [dbo].[UploadBatch]
    ADD CONSTRAINT [PK_Batch] PRIMARY KEY CLUSTERED ([BatchId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[User]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[User] (
    [UserId]                  INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]               NVARCHAR (100) NOT NULL,
    [LastName]                NVARCHAR (100) NOT NULL,
    [Password]                NVARCHAR (250) NOT NULL,
    [Email]                   NVARCHAR (250) NOT NULL,
    [CreateDate]              DATETIME2 (7)  NOT NULL,
    [DisplayName]             NVARCHAR (100) NULL,
    [Deleted]                 BIT            NOT NULL,
    [Username]                NVARCHAR (50)  NOT NULL,
    [LastLogin]               DATETIME2 (7)  NULL,
    [CurrentSession]          DATETIME2 (7)  NULL,
    [AccountStatus]           NVARCHAR (50)  NULL,
    [EnableReceivingOfEmails] BIT            NOT NULL,
    [WebViewMaxWidth]         SMALLINT       NOT NULL,
    [WebViewMaxHeight]        SMALLINT       NOT NULL,
    [MaxInvitations]          TINYINT        NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_User...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[User].[NC_User_Username]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [NC_User_Username]
    ON [dbo].[User]([Username] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Viewed]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[Viewed] (
    [UserId]         INT           NOT NULL,
    [MediaId]        INT           NOT NULL,
    [ViewedDateTime] DATETIME2 (7) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_Viewed...';


GO
ALTER TABLE [dbo].[Viewed]
    ADD CONSTRAINT [PK_Viewed] PRIMARY KEY CLUSTERED ([UserId] ASC, [MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[WaitingList]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[WaitingList] (
    [WaitingListId] INT            IDENTITY (1, 1) NOT NULL,
    [EmailAddress]  NVARCHAR (150) NOT NULL,
    [CreateDate]    DATETIME2 (7)  NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[ExifCollection]...';


GO
CREATE TYPE [dbo].[ExifCollection] AS  TABLE (
    [MediaId] INT            NOT NULL,
    [Key]     INT            NOT NULL,
    [Value]   NVARCHAR (250) NOT NULL);


GO
PRINT N'Creating [dbo].[IdCollection]...';


GO
CREATE TYPE [dbo].[IdCollection] AS  TABLE (
    [Id] INT NOT NULL);


GO
PRINT N'Creating [dbo].[Tags]...';


GO
CREATE TYPE [dbo].[Tags] AS  TABLE (
    [Tag] NVARCHAR (100) NOT NULL);


GO
PRINT N'Creating DF_Album_CreateDate...';


GO
ALTER TABLE [dbo].[Album]
    ADD CONSTRAINT [DF_Album_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Album_LastModifiedDate...';


GO
ALTER TABLE [dbo].[Album]
    ADD CONSTRAINT [DF_Album_LastModifiedDate] DEFAULT (getutcdate()) FOR [LastModifiedDate];


GO
PRINT N'Creating DF_AlbumMedia_CreateDate...';


GO
ALTER TABLE [dbo].[AlbumMedia]
    ADD CONSTRAINT [DF_AlbumMedia_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Configuration_ConfigurationId...';


GO
ALTER TABLE [dbo].[Configuration]
    ADD CONSTRAINT [DF_Configuration_ConfigurationId] DEFAULT (newid()) FOR [ConfigurationId];


GO
PRINT N'Creating DF_File_CreateDate...';


GO
ALTER TABLE [dbo].[File]
    ADD CONSTRAINT [DF_File_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Friend_CreateDate...';


GO
ALTER TABLE [dbo].[Friend]
    ADD CONSTRAINT [DF_Friend_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Invitation_CreateDate...';


GO
ALTER TABLE [dbo].[Invitation]
    ADD CONSTRAINT [DF_Invitation_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Invitation_Sent...';


GO
ALTER TABLE [dbo].[Invitation]
    ADD CONSTRAINT [DF_Invitation_Sent] DEFAULT ((0)) FOR [Sent];


GO
PRINT N'Creating DF_Media_CreateDate...';


GO
ALTER TABLE [dbo].[Media]
    ADD CONSTRAINT [DF_Media_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Media_UploadStatus...';


GO
ALTER TABLE [dbo].[Media]
    ADD CONSTRAINT [DF_Media_UploadStatus] DEFAULT ('Uploading') FOR [UploadStatus];


GO
PRINT N'Creating DF_Photo_Hidden...';


GO
ALTER TABLE [dbo].[Media]
    ADD CONSTRAINT [DF_Photo_Hidden] DEFAULT ((1)) FOR [Status];


GO
PRINT N'Creating DF_MediaLedger_InsertDate...';


GO
ALTER TABLE [dbo].[MediaLedger]
    ADD CONSTRAINT [DF_MediaLedger_InsertDate] DEFAULT (getutcdate()) FOR [InsertDate];


GO
PRINT N'Creating DF_MediaUploadBatch_CreateDate...';


GO
ALTER TABLE [dbo].[MediaUploadBatch]
    ADD CONSTRAINT [DF_MediaUploadBatch_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_MediaViewed_FirstViewed...';


GO
ALTER TABLE [dbo].[MediaViewed]
    ADD CONSTRAINT [DF_MediaViewed_FirstViewed] DEFAULT (getutcdate()) FOR [FirstViewed];


GO
PRINT N'Creating DF_MediaViewed_ViewCount...';


GO
ALTER TABLE [dbo].[MediaViewed]
    ADD CONSTRAINT [DF_MediaViewed_ViewCount] DEFAULT ((1)) FOR [ViewCount];


GO
PRINT N'Creating DF_MediaViewed_ViewDate...';


GO
ALTER TABLE [dbo].[MediaViewed]
    ADD CONSTRAINT [DF_MediaViewed_ViewDate] DEFAULT (getutcdate()) FOR [LastViewed];


GO
PRINT N'Creating DF_RecentActivity_CreateDate...';


GO
ALTER TABLE [dbo].[RecentActivity]
    ADD CONSTRAINT [DF_RecentActivity_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_TagMedia_CreateDate...';


GO
ALTER TABLE [dbo].[TagMedia]
    ADD CONSTRAINT [DF_TagMedia_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Token_CreateDate...';


GO
ALTER TABLE [dbo].[Token]
    ADD CONSTRAINT [DF_Token_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating DF_Token_IsValid...';


GO
ALTER TABLE [dbo].[Token]
    ADD CONSTRAINT [DF_Token_IsValid] DEFAULT ((1)) FOR [IsValid];


GO
PRINT N'Creating DF_User_Deleted...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_Deleted] DEFAULT ((0)) FOR [Deleted];


GO
PRINT N'Creating DF_User_EnableEmails...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_EnableEmails] DEFAULT ((1)) FOR [EnableReceivingOfEmails];


GO
PRINT N'Creating DF_User_MaxInvitation...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_MaxInvitation] DEFAULT ((5)) FOR [MaxInvitations];


GO
PRINT N'Creating DF_User_WebViewMaxHeight...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_WebViewMaxHeight] DEFAULT ((800)) FOR [WebViewMaxHeight];


GO
PRINT N'Creating DF_User_WebViewMaxWidth...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_WebViewMaxWidth] DEFAULT ((800)) FOR [WebViewMaxWidth];


GO
PRINT N'Creating DF_Viewed_ViewedDateTime...';


GO
ALTER TABLE [dbo].[Viewed]
    ADD CONSTRAINT [DF_Viewed_ViewedDateTime] DEFAULT (getutcdate()) FOR [ViewedDateTime];


GO
PRINT N'Creating DF_WaitingList_CreateDate...';


GO
ALTER TABLE [dbo].[WaitingList]
    ADD CONSTRAINT [DF_WaitingList_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];


GO
PRINT N'Creating FK_Album_User...';


GO
ALTER TABLE [dbo].[Album] WITH NOCHECK
    ADD CONSTRAINT [FK_Album_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_AlbumMedia_Album...';


GO
ALTER TABLE [dbo].[AlbumMedia] WITH NOCHECK
    ADD CONSTRAINT [FK_AlbumMedia_Album] FOREIGN KEY ([AlbumId]) REFERENCES [dbo].[Album] ([AlbumId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_AlbumMedia_Media...';


GO
ALTER TABLE [dbo].[AlbumMedia] WITH NOCHECK
    ADD CONSTRAINT [FK_AlbumMedia_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Comment_Media...';


GO
ALTER TABLE [dbo].[Comment] WITH NOCHECK
    ADD CONSTRAINT [FK_Comment_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Exif_Media...';


GO
ALTER TABLE [dbo].[Exif] WITH NOCHECK
    ADD CONSTRAINT [FK_Exif_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Friend_User...';


GO
ALTER TABLE [dbo].[Friend] WITH NOCHECK
    ADD CONSTRAINT [FK_Friend_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Friend_User1...';


GO
ALTER TABLE [dbo].[Friend] WITH NOCHECK
    ADD CONSTRAINT [FK_Friend_User1] FOREIGN KEY ([FriendId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Invitation_User...';


GO
ALTER TABLE [dbo].[Invitation] WITH NOCHECK
    ADD CONSTRAINT [FK_Invitation_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_MediaFile_File...';


GO
ALTER TABLE [dbo].[MediaFile] WITH NOCHECK
    ADD CONSTRAINT [FK_MediaFile_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([FileId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_MediaFile_Media...';


GO
ALTER TABLE [dbo].[MediaFile] WITH NOCHECK
    ADD CONSTRAINT [FK_MediaFile_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_TagMedia_Media...';


GO
ALTER TABLE [dbo].[TagMedia] WITH NOCHECK
    ADD CONSTRAINT [FK_TagMedia_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_TagMedia_Tag...';


GO
ALTER TABLE [dbo].[TagMedia] WITH NOCHECK
    ADD CONSTRAINT [FK_TagMedia_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([TagId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Viewed_Media...';


GO
ALTER TABLE [dbo].[Viewed] WITH NOCHECK
    ADD CONSTRAINT [FK_Viewed_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Viewed_User...';


GO
ALTER TABLE [dbo].[Viewed] WITH NOCHECK
    ADD CONSTRAINT [FK_Viewed_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating [dbo].[Album_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  Album_Delete
( 
	@AlbumId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Album 
	Where AlbumId = @AlbumId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_DeleteAlbumIncludingSubAlbums]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
Create PROCEDURE  [dbo].[Album_DeleteAlbumIncludingSubAlbums]
(
	@UserId int,
	@AlbumId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Declare	@UserId int
 -- Declare @AlbumId int

 -- Set	@UserId = 9
 -- Set	@AlbumId = 5

;WITH Hierarchy(AlbumID, Name, ParentID, HLevel)
AS
(
    SELECT AlbumID, Name, ParentID, 0 as HLevel
    FROM Album 
    WHERE AlbumID = @AlbumId

    UNION ALL

    SELECT SubAlbum.AlbumID, SubAlbum.Name, SubAlbum.ParentID, HLevel + 1
    FROM Album SubAlbum
    INNER JOIN Hierarchy ParentDepartment ON 
    SubAlbum.ParentId = ParentDepartment.AlbumID 
)

Select A.AlbumId, HLevel
Into #tempAlbum
FROM  Hierarchy Inner Join Album A ON
Hierarchy.AlbumID = A.AlbumId
Where A.UserId = @UserId
ORDER BY HLevel DESC

--Select * FROM Album A
--Inner Join #tempAlbum T
--	ON T.AlbumId = A.AlbumId

Delete AM
From AlbumMedia AM
Inner Join #tempAlbum T
	ON T.AlbumId = AM.AlbumId

Delete A
FROM Album A
Inner Join #tempAlbum T
	ON T.AlbumId = A.AlbumId

Drop table #tempAlbum

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_DeletePhotoFromAlbumByPhotoId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Album_DeletePhotoFromAlbumByPhotoId]
	-- Add the parameters for the stored procedure here
(
	@AlbumId int,
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Update Album
	Set CoverMediaId = Null
	Where AlbumId = @AlbumId AND
	CoverMediaId = @MediaId

    -- Insert statements for procedure here
	Delete From AlbumMedia
	Where AlbumId = @AlbumId AND
		  MediaId = @MediaId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Insert]
( 
	@Name nvarchar(50),
	@Description nvarchar(250),
	@UserId int,
	@ParentId int = null,
	@AlbumId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Album] (Name, Description, UserId, ParentId) 
	VALUES (@Name, @Description, @UserId, @ParentId)

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_RetrieveAlbumHierarchyByAlbumIdAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveAlbumHierarchyByAlbumIdAndUserId]
(
	@UserId int,
	@AlbumId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

WITH Hierarchy(AlbumID, Name, ParentID, HLevel)
AS
(
    SELECT AlbumID, Name, ParentID, 0 as HLevel
    FROM Album 
    WHERE AlbumID = @AlbumId

    UNION ALL

    SELECT SubAlbum.AlbumID, SubAlbum.Name, SubAlbum.ParentID, HLevel + 1
    FROM Album SubAlbum
    INNER JOIN Hierarchy ParentDepartment ON 
    SubAlbum.AlbumID = ParentDepartment.ParentID 
)

SELECT A.AlbumID, A.Name, A.ParentID, A.Description, A.UserId, HLevel, 0 as AlbumCount,0 as PhotoCount
FROM  Hierarchy Inner Join Album A ON
Hierarchy.AlbumID = A.AlbumId
Where A.UserId = @UserId

ORDER BY HLevel DESC

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_SetCoverImage]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Album_SetCoverImage
	-- Add the parameters for the stored procedure here
(
	@AlbumId int,
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Album
	Set CoverMediaId = @MediaId
	Where AlbumId = @AlbumId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_Update]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Update]
( 
	@AlbumId int,
	@Name nvarchar(50),
	@Description nvarchar(250),
	@UserId int,
	@ParentId int = null
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Album 
	SET 
	Name = @Name,
	Description = @Description,
	UserId = @UserId,
	ParentId = @ParentId
 
	Where AlbumId = @AlbumId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_UpdateMediaPosition]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Album_UpdateMediaPosition]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@Position int,
	@MediaId int,
	@AlbumId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update AlbumMedia Set Position = @Position
	From AlbumMedia inner join 
	(
		Select AlbumId 
		From Album 
		Where Album.AlbumId = @AlbumId AND Album.UserId = @UserId
	) A On
	AlbumMedia.AlbumId = A.AlbumId
	Where AlbumMedia.MediaId = @MediaId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[AlbumMedia_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  AlbumMedia_Delete
( 
	@AlbumId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From AlbumMedia 
	Where AlbumId = @AlbumId And MediaId = @MediaId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[AlbumMedia_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[AlbumMedia_Insert]
( 
	@AlbumId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[AlbumMedia] (AlbumId, MediaId) 
	VALUES (@AlbumId, @MediaId)

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[AlbumMedia_SelectAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  AlbumMedia_SelectAll

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select AlbumId, MediaId 
	From AlbumMedia

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[AlbumMedia_SelectByPrimaryKey]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  AlbumMedia_SelectByPrimaryKey
( 
	@AlbumId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select AlbumId, MediaId 
	From AlbumMedia 
	Where AlbumId = @AlbumId And MediaId = @MediaId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[AlbumMedia_Update]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  AlbumMedia_Update
( 
	@AlbumId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update AlbumMedia 
	SET 	AlbumId = @AlbumId,
	MediaId = @MediaId
 
	Where AlbumId = @AlbumId And MediaId = @MediaId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  Comment_Delete
( 
	@CommentId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Comment 
	Where CommentId = @CommentId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_Insert]
( 
	@Name nvarchar(100),
	@Email nvarchar(255),
	@SiteUrl nvarchar(100),
	@Ip nvarchar(100),
	@UserAgent nvarchar(500),
	@CommentStatus nvarchar(50),
	@Text nvarchar(max),
	@CommentDate datetime,
	@UserId int,
	@ParentId int,
	@MediaId int,
	@Identity int output
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Comment] (Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text],  CommentDate, UserId, ParentId, MediaId) 
	VALUES (@Name, @Email, @SiteUrl, @Ip, @UserAgent, @CommentStatus, @Text, @CommentDate, @UserId, @ParentId, @MediaId)
	
	Set @Identity =(Select @@IDENTITY)
	return @Identity	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_RetrieveAllByMediaId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO

CREATE PROCEDURE  [dbo].[Comment_RetrieveAllByMediaId]
(
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text], CommentDate, UserId, ParentId, MediaId 
	From Comment
	Where MediaId = @MediaId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_RetrieveCommentsByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveCommentsByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text], CommentDate, Media.UserId, ParentId, Media.MediaId 
	From Comment Inner Join Media On
	Comment.MediaId = Media.MediaId
	Where Media.UserId = @UserId AND CommentStatus <> 'Deleted'

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_RetrieveCommentStatusCountByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveCommentStatusCountByUserId]
( 
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--declare @UserId int
	--set @UserId = 9
	
	DECLARE @CommentStatus TABLE
	(
	  CommentStatus nvarchar(50),
	  MediaCount int
	)
	
	--Approved
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Approved' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Approved'
	
	--Pending
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Pending' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Pending'
	
	--Deleted
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Deleted' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Deleted'
	
	--Spam
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Spam' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Spam'

	--Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text],  CommentDate, UserId, ParentId, MediaId 
	--From Comment 
	--Where CommentId = @CommentId 
	
	Select * from @CommentStatus
	
	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_RetrieveRecent5CommentsByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveRecent5CommentsByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(5) CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text], CommentDate, M.UserId, ParentId, M.MediaId 
	From Comment C Inner Join Media M On M.MediaId = C.MediaId
	Where M.UserId = @UserId AND CommentStatus = 'Approved'
	Order by CommentDate

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_SelectAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_SelectAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text], CommentDate, UserId, ParentId, MediaId 
	From Comment

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_SelectByPrimaryKey]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_SelectByPrimaryKey]
( 
	@CommentId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text],  CommentDate, UserId, ParentId, MediaId 
	From Comment 
	Where CommentId = @CommentId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_Update]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_Update]
( 
	@CommentId int,
	@Name nvarchar(100),
	@Email nvarchar(255),
	@SiteUrl nvarchar(100),
	@Ip nvarchar(100),
	@UserAgent nvarchar(500),
	@CommentStatus nvarchar(50),
	@Text nvarchar(max),
	@CommentDate datetime,
	@UserId int,
	@ParentId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Comment 
	SET 	
	Name = @Name,
	Email = @Email,
	SiteUrl = @SiteUrl,
	Ip = @Ip,
	UserAgent = @UserAgent,
	CommentStatus = @CommentStatus,
	[Text] = @Text,
	CommentDate = @CommentDate,
	UserId = @UserId,
	ParentId = @ParentId,
	MediaId = @MediaId
 
	Where CommentId = @CommentId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_UpdateStatus]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
Create PROCEDURE  [dbo].[Comment_UpdateStatus]
( 
	@CommentId int,
	@CommentStatus nvarchar(50),
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Comment 
	SET CommentStatus = @CommentStatus
	From Comment C Inner Join Media M
	On C.MediaId = M.MediaId 
	Where C.CommentId = @CommentId AND M.UserId = @UserId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Configuration_SelectAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  Configuration_SelectAll

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select ConfigurationId, SiteName, SiteTagLine, Membership, ThumbNailHeight, ThumbNailWidth, WebHeight, WebWidth 
	From Configuration

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[File_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  File_Delete
( 
	@FileId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From [File]
	Where FileId = @FileId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[File_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  File_Insert
( 
	@FilePath nvarchar(255),
	@FileExtension nvarchar(10),
	@OriginalFileName nvarchar(200),
	@MediaId int,
	@Identity int output,
	@MediaFormat nvarchar(50),
	@MediaType nvarchar(50)
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[File] (FilePath, FileExtension, OriginalFileName, CreateDate) 
	VALUES (@FilePath, @FileExtension, @OriginalFileName, GETUTCDATE())	

	Set @Identity =(Select @@IDENTITY)	
	Insert INTO MediaFile(MediaId, FileId, MediaFormat, MediaType) VALUES (@MediaId, @Identity, @MediaFormat, @MediaType) 
	
	return @Identity

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[File_SelectAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  File_SelectAll

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select FileId, FilePath, FileExtension, OriginalFileName, CreateDate 
	From [File]

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[File_SelectByPrimaryKey]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  File_SelectByPrimaryKey
( 
	@FileId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select FileId, FilePath, FileExtension, OriginalFileName, CreateDate 
	From [File]
	Where FileId = @FileId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[File_Update]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  File_Update
( 
	@FileId int,
	@FilePath nvarchar(255),
	@FileExtension nvarchar(10),
	@OriginalFileName nvarchar(200)
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update [File]
	SET
	FilePath = @FilePath,
	FileExtension = @FileExtension,
	OriginalFileName = @OriginalFileName
 
	Where FileId = @FileId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Friend_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 28, 2009
-- =============================================
CREATE PROCEDURE  Friend_Delete
(
	@FriendId int,
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Friend
	Where FriendId = @FriendId AND UserId = @UserId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Friend_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 28, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Friend_Insert]
( 
	@FriendId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Friend] (FriendId, UserId, CreateDate) 
	VALUES (@FriendId, @UserId, GETUTCDATE())

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Friend_SearchByText]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Friend_SearchByText]
	-- Add the parameters for the stored procedure here
(
	@Search nvarchar(250),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--declare @Search nvarchar(250)
	--declare @UserId int
   
 --  Set @Search = 'c'
 --  Set @UserId = 10
	
	-- Insert statements for procedure here
	SELECT     U.FirstName, U.LastName, U.Email, U.DisplayName, U.Username, U.UserId as FriendId,
							 (SELECT   COUNT(Media.MediaId)
								FROM          Media
								WHERE      UserId = U.UserId) AS PhotoCount,
							 COALESCE((SELECT  TOP (1) ROW_NUMBER() OVER (ORDER BY TagId ASC) AS C 
	FROM         (SELECT     TagMedia.MediaId, TM.TagId
						   FROM          (SELECT     TagId
												   FROM          TagMedia
												   GROUP BY TagId) TM INNER JOIN
												  TagMedia ON TM.TagId = TagMedia.TagId INNER JOIN
												  Media ON TagMedia.MediaId = Media.MediaId
						   WHERE      UserId = U.UserId) E
	GROUP BY TagId
	ORDER BY C DESC), 0) AS TagCount,
		(SELECT     COUNT(*) AS C
		  FROM          Friend
		  WHERE      UserId = U.UserId) AS FriendCount
	FROM [User] U left join Friend F On
	U.UserId = F.FriendId 
	Where U.UserId <> @UserId AND U.UserId Not in (Select FriendId as UserId From Friend Where UserId = @UserId) AND 
					([FirstName] LIKE '%' + @Search + '%' 
					OR [LastName] LIKE '%' + @Search + '%' 
					OR Email LIKE '%' + @Search + '%' 
					OR DisplayName LIKE '%' + @Search + '%'
					OR Username LIKE '%' + @Search + '%')				
			


END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[InsertTagWithMediaId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertTagWithMediaId 
(
	@TagName nvarchar(100),
	@mediaId int
)
AS
BEGIN
Declare @TagId int
if EXISTS(SELECT TG.TagId, TG.TagName, TG.MediaId
	FROM  (
           Select dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId From dbo.Tag INNER JOIN
					dbo.TagMedia ON dbo.Tag.TagId = dbo.TagMedia.TagId
					GROUP BY dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId
					
				)TG
			WHERE  TG.TagName = @TagName AND MediaId = @MediaId
		)
	Begin
		 Set @tagId = (
						SELECT TG.TagId
						FROM	(
									Select dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId From  dbo.Tag INNER JOIN
									dbo.TagMedia ON dbo.Tag.TagId = dbo.TagMedia.TagId
									GROUP BY dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId
								) TG
						WHERE  TG.TagName = @TagName AND MediaId = @MediaId
					   )

		Insert Into TagMedia(TagID,MediaId) Values (@tagId, @MediaId)
	End
ELSE
	Begin
		Insert INTO Tag(TagName)Values(@TagName)
		Declare @newTagId int
		set @newTagId = (Select @@IDENTITY)

		Insert Into TagMedia(TagID,MediaId) Values (@newTagId, @MediaId)
	End
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Invitation_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Invitation_Insert
	-- Add the parameters for the stored procedure here
(
	@Email nvarchar(250),
	@UserId int

)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO Invitation(Email, UserId)VALUES(@Email, @UserId)	
	
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Invitation_UserInvitationsRemaining]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Invitation_UserInvitationsRemaining 
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    Declare @InvitationCount int
    Set @InvitationCount = 
    (
		Select COUNT(*) 
		From Invitation
		Where Invitation.UserId = @UserId
	)
    
    Declare @MaxInvitations int
	Set @MaxInvitations =
	(
		Select MaxInvitations 
		From dbo.[User]
		Where dbo.[User].UserId = @UserId
	)
	
	Declare @Remaining int
	Set @Remaining = (@MaxInvitations - @InvitationCount)
	
	IF (@Remaining < 0)
	BEGIN
	   set	@Remaining = 0
	END
	
	Select @Remaining as RemainingInvitations

	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Location_AutocompleteSearch]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Location_AutocompleteSearch
	-- Add the parameters for the stored procedure here
(
	@Text nvarchar(100),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select Top(10) *
    From Location
    Where UserId = @UserId 
    AND LocationName Like '%' + @Text + '%'
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Location_Save]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Location_Save
	-- Add the parameters for the stored procedure here
(
	@LocationId int,
	@LocationName nvarchar(100),
	@Latitude numeric(26, 15),
	@Longitude numeric(26, 15),
	@UserId int,
	@Zoom tinyint,
	@MapTypeId nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @LocationId > 0
	BEGIN
		Update Location
		Set LocationName = @LocationName,
			Latitude = @Latitude,
			Longitude = @Longitude,
			UserId = @UserId,
			Zoom = @Zoom,
			MapTypeId = @MapTypeId
		Where LocationId = @LocationId
	END
	ELSE
	BEGIN
	
		IF EXists(Select * 
			      From Location 
			      Where LocationName = @LocationName
			      AND UserId = @UserId)
		BEGIN
				Update Location
				Set LocationName = @LocationName,
					Latitude = @Latitude,
					Longitude = @Longitude,
					UserId = @UserId,
					Zoom = @Zoom,
					MapTypeId = @MapTypeId
			     Where LocationName = @LocationName
			     AND UserId = @UserId		
		END 
		ELSE
		BEGIN
		
			Insert Into Location (LocationName, Latitude, Longitude, UserId, Zoom, MapTypeId)
			Values (@LocationName, @Latitude, @Longitude, @UserId, @Zoom, @MapTypeId)
		
		END	
	END
    
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Delete]
( 
	@MediaId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
BEGIN TRY
	Begin Transaction
	
	--CREATE TABLE #tempFile
	--(
	--   FileId INT
	--)
	
	Select F.FileId 
	INTO #tempFile
	From [File] F
	Inner Join MediaFile MF
		On F.FileId = MF.FileId
	Where MF.MediaId = @MediaId
	
	Delete From MediaFile
	Where MediaId = @MediaId
	
	Delete F
	From [File] F
	Inner Join #tempFile MF
		On F.FileId = MF.FileId 	
	
	Delete From MediaFile
	Where MediaId = @MediaId	
	
	
	Delete From TagMedia
	Where MediaId = @MediaId
	
	Delete From Comment
	Where MediaId = @MediaId
	
	Delete From AlbumMedia
	Where MediaId = @MediaId
	
	Delete From Media 
	Where MediaId = @MediaId AND UserId = @UserId
	
	drop table #tempFile
		
	Commit
	
	END TRY
BEGIN CATCH
	Rollback
	DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    -- Use RAISERROR inside the CATCH block to return error
    -- information about the original error that caused
    -- execution to jump to the CATCH block.
    RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );
END CATCH

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Insert]
( 
	@Title nvarchar(100),
	@Description nvarchar(550),
	--@TimesViewed int,
	@MediaYear int = null,
	@MediaMonth int = null,
	@MediaDay int = null,
	@UserId int,
	@Status nvarchar(50),
	@Tags nvarchar(250),
	@Identity int output
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Media] (UserId, Status) 
	VALUES (@UserId, @Status)
	
	Set @Identity =(Select @@IDENTITY)
	
	Insert INTO MediaLedger(Title, Description, Tags, MediaDay, MediaYear, MediaMonth, MediaId, UserId)
	VALUES(@Title, @Description, @Tags, @MediaDay, @MediaYear, @MediaMonth, @Identity, @UserId)
	
	return @Identity	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RemoveTag]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Media_RemoveTag
	-- Add the parameters for the stored procedure here
(
	@MediaId int,
	@TagName nvarchar(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete TM
	From Tag T
	Inner Join TagMedia TM
		ON T.TagId = TM.TagId
	Where TM.MediaId = @MediaId
	AND TagName = @TagName
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_Update]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Update]
( 
	@Title nvarchar(100),
	@Description nvarchar(550),
	--@TimesViewed int,
	@MediaYear int = null,
	@MediaMonth int = null,
	@MediaDay int = null,
	@UserId int,
	@Status nvarchar(50),
	@Tags nvarchar(250),
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Media 
	SET 	
	UserId = @UserId,
	Status = @Status 
	Where MediaId = @MediaId  AND UserId = @UserId
	
	Insert Into MediaLedger(Title, Description, Tags, MediaYear, MediaMonth, MediaDay, MediaId, UserId)
	Values (@Title, @Description, @Tags, @MediaYear, @MediaMonth, @MediaDay, @MediaId, @UserId)

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_UpdateStatus]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[Media_UpdateStatus]
( 
	@UserId int,
	@Status nvarchar(50),
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Media 
	SET 	
	Status = @Status 
	Where MediaId = @MediaId  AND UserId = @UserId


END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaQueue_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE MediaQueue_Delete
	-- Add the parameters for the stored procedure here
(
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From MediaQueue 
	Where MediaQueue.MediaId = @MediaId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaQueue_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaQueue_Insert] 
	-- Add the parameters for the stored procedure here
(
	@MediaId int,
	@MediaBytes varbinary(max),
	@Filename nvarchar(250)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO MediaQueue(MediaId, MediaBytes, [Filename])VALUES(@MediaId, @MediaBytes, @Filename)
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaQueue_InsertExif]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE MediaQueue_InsertExif
(	-- Add the parameters for the stored procedure here
	@ExifCollection ExifCollection readonly
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Insert Into Exif(MediaId, [Key], Value)
	Select MediaId, [Key], Value
	From @ExifCollection	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaQueue_RetrieveByMediaId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaQueue_RetrieveByMediaId] 
(
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select MediaId, MediaQueueId, MediaBytes, [Filename]
	From MediaQueue
	Where MediaId = @MediaId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaUploadBatch_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaUploadBatch_Insert]
	-- Add the parameters for the stored procedure here
(
	@MediaId int,
	@BatchId uniqueidentifier
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert Into MediaUploadBatch (MediaId, UploadBatch) Values(@MediaId, @BatchId)
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaViewed_IncrementMediaCount]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaViewed_IncrementMediaCount]
	-- Add the parameters for the stored procedure here
	@MediaId int,
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF Exists(
				Select * 
				From MediaViewed 
				Where MediaId = @MediaId AND UserId = @UserId
			  )
		BEGIN
		
			Update MediaViewed
			Set ViewCount = (ViewCount + 1),
			LastViewed = (getutcdate())
			Where MediaId = @MediaId AND UserId = @UserId
		
		END
	ELSE
		BEGIN
			Insert Into MediaViewed(MediaId, UserId) Values (@MediaId, @UserId)
		END
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Paging_RetrieveNextPreviousMedia]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrieveNextPreviousMedia]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @Current int
	Declare @Next int
	Declare @Previous int

   	DECLARE @NextPrevious TABLE
	(
	  NextId int,
	  PreviousId int
	)
	
	Declare @Media Table
	(
	  MediaId int,
	  RowNumber int,
	  CreateDate datetime2
	)
    
    ;WITH MediaRow AS
	( 
		SELECT MediaId, UserId, CreateDate,
		ROW_NUMBER() OVER (ORDER BY CreateDate desc) AS RowNumber
		FROM Media 
		Where UserId = 9 And Status = 'Public'
	)

	Insert INTO @Media(MediaId,RowNumber, CreateDate)
	Select MediaId, RowNumber, CreateDate
	From MediaRow
	
	
	Set @Current = (Select RowNumber From @Media Where MediaId = @MediaId)
	Set @Next = (Select MediaId From @Media Where RowNumber = (@Current - 1)  )
	Set @Previous = (Select MediaId From @Media Where RowNumber = (@Current + 1) )
	
	Insert INTO @NextPrevious(NextId, PreviousId)VALUES(IsNull(@Next, -1), IsNull(@Previous, -1))	
	
	Select * From @NextPrevious
    
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Paging_RetrievePagingByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrievePagingByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		Declare @Paging Table  (PageIndex VarChar(3), PageText VarChar(3)) 	

	Begin
			Declare @StartIndex Int, @EndIndex Int, @Incr Int  
		
			Set @StartIndex = 0 
			Set @EndIndex = 0
			Set @Incr = 0  
			Select @StartIndex = Ascii('A')  
			Select @EndIndex = Ascii('Z')  
			Select @Incr = @StartIndex  
			While (@EndIndex >= @Incr )  
			Begin  
				Insert Into @Paging  
				Select Char(@Incr), Char(@Incr)  
				Select @Incr = @Incr+1  
			End  
	End

	Select [@Paging].PageIndex, COALESCE(MediaCountInLetter, 0) as MediaCountInLetter
	From
	(
		Select PageText, COUNT(PageIndex)AS MediaCountInLetter 
		From (
		   Select TagId, TagName, Tags.MediaId, Media.UserId
		   From
		   (			Select TagMedia.TagId, TagName, TagMedia.MediaId
						From Tag inner join TagMedia 
						ON Tag.TagId = TagMedia.TagId 
		   ) as Tags Inner Join Media 
		   ON Tags.MediaId = Media.MediaId
		   Where UserId = @UserId
	) as TagsAndMedia Left Join @Paging ON Upper(Left(TagName,1)) = PageText
	Group by PageText) As LetterCounts Right Join @Paging 
	On LetterCounts.PageText = [@Paging].PageIndex
	END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Paging_RetrievePagingByUserIdAndLetter]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrievePagingByUserIdAndLetter]
(
	@Letter char,
	@UserId int,
	@PageSize int,
	@CurrentPage int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

    -- Insert statements for procedure here
		Declare @Paging Table  (PageIndex VarChar(3), PageText VarChar(3))		
		Insert Into @Paging( PageIndex, PageText)Values(@Letter,@Letter)
		
		--Minus 1 from the current page. There is no page 0, the code however is based on a zero based index.
		Declare @Head int = @PageSize * (@CurrentPage - 1)
		Declare @Tail int = @Head + @PageSize

		Select * From
		( Select ROW_NUMBER() Over (Order By TagName) AS rownum, TagId, TagName, [Description], PageText, 0 as TagCount
			From (
		   Select TagId, TagName, PageText, [Description]
				From (
				   Select TagId, TagName, Tags.MediaId, [Description], Media.UserId
				   From
				   (			Select TagMedia.TagId, TagName, [Description], TagMedia.MediaId
								From Tag inner join TagMedia 
								ON Tag.TagId = TagMedia.TagId 
				   ) as Tags Inner Join Media 
				   ON Tags.MediaId = Media.MediaId
				   Where UserId = @UserId
			) as TagsAndMedia inner Join @Paging ON Upper(Left(TagName,1)) = PageText
			--Where PageText <> Null
			Group by PageText, TagName, TagId, [Description]
			) as FilteredTags) RowFilter
		Where RowFilter.rownum > @Head AND RowFilter.rownum <= @Tail
	END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Paging_RetrievePagingCountByUserIdAndLetter]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrievePagingCountByUserIdAndLetter]
(
	@Letter char,
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
  --Declare @Letter char
  --Declare @UserId int
  --Declare @PageSize int
  --Declare @CurrentPage int
  
  --set @Letter = 'J'
  --set @UserId = 9
  --set @PageSize = 5
  --set @CurrentPage = 1

    -- Insert statements for procedure here
		Declare @Paging Table  (PageIndex VarChar(3), PageText VarChar(3))		
		Insert Into @Paging( PageIndex, PageText)Values(@Letter,@Letter)
		
		--Minus 1 from the current page. There is no page 0, the code however is based on a zero based index.
		--Declare @Head int = @PageSize * (@CurrentPage - 1)
		--Declare @Tail int = @Head + @PageSize

		Select Count(PageText) as TagCount From
		( Select ROW_NUMBER() Over (Order By TagName) AS rownum, TagId, TagName, PageText, 0 as TagCount
			From (
		   Select TagId, TagName, PageText
				From (
				   Select TagId, TagName, Tags.MediaId, Media.UserId
				   From
				   (			Select TagMedia.TagId, TagName, TagMedia.MediaId
								From Tag inner join TagMedia 
								ON Tag.TagId = TagMedia.TagId 
				   ) as Tags Inner Join Media 
				   ON Tags.MediaId = Media.MediaId
				   Where UserId = @UserId
			) as TagsAndMedia inner Join @Paging ON Upper(Left(TagName,1)) = PageText
			--Where PageText <> Null
			Group by PageText, TagName, TagId
			) as FilteredTags) RowFilter
			group by PageText
	END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Recent_GetTimelineForUser]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Recent_GetTimelineForUser
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select E.*, M.CreateDate, M.Status, M.UploadStatus, M.UserId
Into #MediaWithYear 
From (Select	MediaId,
			Title,
			Description,
			Tags, 
			MediaDay, 
			MediaYear, 
			MediaMonth
			
	FROM          
			(SELECT MediaId, 
					InsertDate, 
					Title, 
					Description, 
					Tags, 
					MediaDay, 
					MediaYear,
					MediaMonth, 
					row_number() OVER	(
											partition BY MEdiaID 
											ORDER BY InsertDate DESC			
										) AS rn
					FROM dbo.MediaLedger
		    ) AS T
	WHERE rn = 1) E 
	Inner Join Media M
		ON E.MediaId = M.MediaId
	Where E.MediaYear is Not Null and M.UserId = 9
	
	Select MediaYear, COUNT(MediaYear) as count
	From #MediaWithYear
	group by MediaYear
	Order By MediaYear
	
	Drop table #MediaWithYear
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Recent_GetYearsForUser]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Recent_GetYearsForUser]
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select E.*, M.CreateDate, M.Status, M.UploadStatus, M.UserId
Into #MediaWithYear 
From (Select	MediaId,
			Title,
			Description,
			Tags, 
			MediaDay, 
			MediaYear, 
			MediaMonth
			
	FROM          
			(SELECT MediaId, 
					InsertDate, 
					Title, 
					Description, 
					Tags, 
					MediaDay, 
					MediaYear,
					MediaMonth, 
					row_number() OVER	(
											partition BY MEdiaID 
											ORDER BY InsertDate DESC			
										) AS rn
					FROM dbo.MediaLedger
		    ) AS T
	WHERE rn = 1) E 
	Inner Join Media M
		ON E.MediaId = M.MediaId
	Where E.MediaYear is Not Null and M.UserId = @UserId
	
	Select MediaYear, COUNT(MediaYear) as count
	From #MediaWithYear
	group by MediaYear
	Order By MediaYear
	
	Drop table #MediaWithYear
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[RecentActivity_OneHourActivityRollUp]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecentActivity_OneHourActivityRollUp]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--DECLARE @ActivityTable TABLE
	--(
	--  ActivityType nvarchar(50),
	--  ActivityCount int
	--)
	
	--Photos added
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'PhotosAdded' as ActivityType, COUNT(*) as ActivityCount, UserId From Media Where CreateDate > DATEADD(HOUR , -1, GETUTCDATE() ) Group By UserId 
	
	--Friends
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'FriendsAdded' as ActivityType, COUNT(*) as ActivityCount, UserId From Friend Where CreateDate > DATEADD(HOUR , -1, GETUTCDATE() ) Group By UserId
	
	--Albums
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'NewAlbums' as ActivityType, COUNT(*) as ActivityCount, UserId From Album Where CreateDate > DATEADD(HOUR , -1, GETUTCDATE() ) Group By UserId
	
	--Comments
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'NewComments' as ActivityType, COUNT(*) as ActivityCount, UserId 
	From Comment C 
	Where CommentDate > DATEADD(HOUR , -1, GETUTCDATE())
	Group By UserId
	
	-- Album Meida
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'AddedPhotosToAlbums' as ActivityType, COUNT(*) as ActivityCount, UserId From AlbumMedia AM inner join Album A On A.AlbumId = AM.AlbumId 
	Where  AM.CreateDate > DATEADD(HOUR , -1, GETUTCDATE()) Group By UserId
	
	-- Tags Added
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'TagsAddedToPhotos' as ActivityType, COUNT(*) as ActivityCount, UserId 
	From TagMedia TM inner join Media M On TM.MediaId = M.MediaId 
	Where  TM.CreateDate > DATEADD(HOUR , -1, GETUTCDATE()) Group By UserId

    
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[RecentActivity_RetrieveOneMonthActivityRollUp]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecentActivity_RetrieveOneMonthActivityRollUp]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--DECLARE @ActivityTable TABLE
	--(
	--  ActivityType nvarchar(50),
	--  ActivityCount int
	--)
	
	----Photos added
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'PhotosAdded' as ActivityType, COUNT(*) as ActivityCount From Media Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE() ) 
	
	----Friends
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'FriendsAdded' as ActivityType, COUNT(*) as ActivityCount From Friend Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE() ) 
	
	----Albums
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'NewAlbums' as ActivityType, COUNT(*) as ActivityCount From Album Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE() ) 
	
	----Comments
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'NewComments' as ActivityType, COUNT(*) as ActivityCount 
	--From Comment C 
	--Where UserId = @UserId AND 
	--CommentDate > DATEADD(DAY , -30, GETUTCDATE())
	
	---- Album Meida
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'AddedPhotosToAlbums' as ActivityType, COUNT(*) as ActivityCount From AlbumMedia AM inner join Album A On A.AlbumId = AM.AlbumId Where A.UserId = @UserId AND AM.CreateDate > DATEADD(DAY , -30, GETUTCDATE()) 
	
	---- Tags Added
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'TagsAddedToPhotos' as ActivityType, COUNT(*) as ActivityCount From TagMedia TM inner join Media M On TM.MediaId = M.MediaId Where M.UserId = @UserId AND TM.CreateDate > DATEADD(DAY , -30, GETUTCDATE()) 
	
	Select ActivityType, Sum(ActivityCount) as 'ActivityCount', UserId
	From RecentActivity	
	Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE())
	Group By UserId, ActivityType 


    
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Reporting_SiteStatistics]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Reporting_SiteStatistics] 
	-- Add the parameters for the stored procedure here
(
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Statistics TABLE
	(
	  Name nvarchar(50),
	  [Count] int
	)
	
	--Friends
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Friends' as Name
	From Friend
	Where UserId = @UserId
	
	--Media/Photos
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Photos' as Name
	From Media
	Where UserId = @UserId
	
	--Albums
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Albums' as Name
	From Album
	Where UserId = @UserId
	
	--Tags
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Tags' as Name
	From 
	(
		Select COUNT(*) AS TagCount From Tag T Inner join TagMedia TM
		ON T.TagId = TM.TagId Inner Join Media M
		ON TM.MediaId = M.MediaId
		Where UserId = 9
		Group By TM.TagId
	) C
	
	
	
	Select Name, [Count]
	From @Statistics
	ORder By Name
	
	 
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_DeleteTagsByMediaId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Tag_DeleteTagsByMediaId
	-- Add the parameters for the stored procedure here
	(
		@MediaId int
	)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From TagMedia Where MediaId = @MediaId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_GetRelatedTags]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_GetRelatedTags] 
	-- Add the parameters for the stored procedure here
(
	@TagId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select T.*, 0 as TagCount 
	From (
		Select TM.TagId 
		From 
			(
				Select *
				From TagMedia
				Where TagId = @TagId
			) M
		Inner Join TagMedia TM
			ON M.MediaId = TM.MediaId
		Where TM.TagId <> @TagId
		Group by TM.TagId
	  ) R
	Inner Join Tag T
	ON R.TagId = T.TagId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_InsertTagWithMediaId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_InsertTagWithMediaId] 
(
	@TagName nvarchar(100),
	@mediaId int
)
AS
BEGIN
Declare @TagId int
if EXISTS(SELECT TagName
			FROM dbo.Tag  
			WHERE  TagName = @TagName
		)
	Begin
		 Set @tagId = (
						SELECT TagId
						FROM dbo.Tag
						WHERE  TagName = @TagName
					   )
	   

		Insert Into TagMedia(TagID,MediaId) Values (@tagId, @MediaId)
	End
ELSE
	Begin
		Insert INTO Tag(TagName)Values(@TagName)
		Declare @newTagId int
		set @newTagId = (Select @@IDENTITY)

		Insert Into TagMedia(TagID,MediaId) Values (@newTagId, @MediaId)
	End
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_RetrieveTagByNameAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Tag_RetrieveTagByNameAndUserId 
	-- Add the parameters for the stored procedure here
(
	@TagName nvarchar(100),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Declare @TagName nvarchar(100)
	--Declare @UserId int
	
	--set @TagName = 'erin-meraz'
	--set @UserId = 9

    -- Insert statements for procedure here
    
    Select R.TagId, TagName, [Description], 0 as TagCount 
    From (    
			Select T.TagId
			From Tag T inner join TagMedia TM
			ON T.TagId = TM.TagId Inner Join Media M
			ON TM.MediaId = M.MediaId
			Where T.TagName = @TagName AND M.UserId = @UserId
			Group By T.TagId
		) R inner join Tag T
	ON T.TagId = R.TagId
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_RetrieveTagsByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_RetrieveTagsByUserId]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@Visibility nvarchar(50) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--WITH tm (TagId, MediaId, UserId)
	--AS
	--(		

		
	--  Select * From(Select Tag.TagId From Tag Inner join TagMedia On
	--	Tag.TagId = TagMedia.TagId
	--	Group By Tag.TagId) T Inner Join On 
		
		
		
		
	-- select TagId, MediaId, UserId from(	
	--	 Select TagId, media.MediaId, UserId 
	--	 From TagMedia Inner Join  Media On
	--	 TagMedia.MediaId = Media.MediaId
	--	 Group By TagId, Media.MediaId, UserId)T
	--	 Group by TagId, MediaId, UserId
	--)


 --   -- Insert statements for procedure here
	--SELECT  Tag.TagId, TagName
	--FROM Tag Inner Join tm ON
	--Tag.TagId = tm.TagId 
	Select Tag.TagId, TagName, TagCount, [Description] From Tag Inner Join (Select TagId, UserId, COUNT(TagId) as TagCount From TagMedia Inner Join Media On 
	TagMedia.MediaId = Media.MediaId
	Where Media.Status = @Visibility
	Group By TagId, UserId) TM ON
	Tag.TagId = TM.TagId                
	Where UserId = @UserId
	Order by TagName asc
    
    	--Select tagId From Tag Group By TagId) T INNER JOIN
     --                 dbo.TagMedia ON T.TagId = dbo.TagMedia.TagId INNER JOIN
     --                 dbo.Media ON dbo.TagMedia.MediaId = dbo.Media.MediaId
     --      Group By dbo.TagMedia.TagId, T.TagId, dbo.Media.UserId ) G  
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_RetrieveTagsByUserIdAndTagId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_RetrieveTagsByUserIdAndTagId]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@TagId int,
	@Visibility nvarchar(50) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--WITH tm (TagId, MediaId, UserId)
	--AS
	--(		

		
	--  Select * From(Select Tag.TagId From Tag Inner join TagMedia On
	--	Tag.TagId = TagMedia.TagId
	--	Group By Tag.TagId) T Inner Join On 
		
		
		
		
	-- select TagId, MediaId, UserId from(	
	--	 Select TagId, media.MediaId, UserId 
	--	 From TagMedia Inner Join  Media On
	--	 TagMedia.MediaId = Media.MediaId
	--	 Group By TagId, Media.MediaId, UserId)T
	--	 Group by TagId, MediaId, UserId
	--)


 --   -- Insert statements for procedure here
	--SELECT  Tag.TagId, TagName
	--FROM Tag Inner Join tm ON
	--Tag.TagId = tm.TagId 
	Select Tag.TagId, TagName, TagCount, [Description] From Tag Inner Join (Select TagId, UserId, COUNT(TagId) as TagCount From TagMedia Inner Join Media On 
	TagMedia.MediaId = Media.MediaId
	Where Media.Status = @Visibility
	Group By TagId, UserId) TM ON
	Tag.TagId = TM.TagId                
	Where UserId = @UserId AND Tag.TagId = @TagId
	Order by TagName asc
    
    	--Select tagId From Tag Group By TagId) T INNER JOIN
     --                 dbo.TagMedia ON T.TagId = dbo.TagMedia.TagId INNER JOIN
     --                 dbo.Media ON dbo.TagMedia.MediaId = dbo.Media.MediaId
     --      Group By dbo.TagMedia.TagId, T.TagId, dbo.Media.UserId ) G  
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_Search]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_Search]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@SearchText nvarchar(50)	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--WITH tm (TagId, MediaId, UserId)
	--AS
	--(		

		
	--  Select * From(Select Tag.TagId From Tag Inner join TagMedia On
	--	Tag.TagId = TagMedia.TagId
	--	Group By Tag.TagId) T Inner Join On 
		
		
		
		
	-- select TagId, MediaId, UserId from(	
	--	 Select TagId, media.MediaId, UserId 
	--	 From TagMedia Inner Join  Media On
	--	 TagMedia.MediaId = Media.MediaId
	--	 Group By TagId, Media.MediaId, UserId)T
	--	 Group by TagId, MediaId, UserId
	--)


 --   -- Insert statements for procedure here
	--SELECT  Tag.TagId, TagName
	--FROM Tag Inner Join tm ON
	--Tag.TagId = tm.TagId 
	Select Top 10 Tag.TagId, TagName, TagCount, [Description]  From Tag Inner Join (Select TagId, UserId, COUNT(TagId) as TagCount From TagMedia Inner Join Media On 
	TagMedia.MediaId = Media.MediaId
	Group By TagId, UserId) TM ON
	Tag.TagId = TM.TagId                
	Where UserId = @UserId AND TagName Like '%'+ @SearchText + '%'
	Order by TagName asc
    
    	--Select tagId From Tag Group By TagId) T INNER JOIN
     --                 dbo.TagMedia ON T.TagId = dbo.TagMedia.TagId INNER JOIN
     --                 dbo.Media ON dbo.TagMedia.MediaId = dbo.Media.MediaId
     --      Group By dbo.TagMedia.TagId, T.TagId, dbo.Media.UserId ) G  
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_Update]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Tag_Update]
	-- Add the parameters for the stored procedure here
(
	@TagId int,
	@UserId int,
	@Description nvarchar(500),
	@TagName nvarchar(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    Update Tag 
    Set [Description] = @Description,
    TagName = @TagName
    From Tag T inner join TagMedia TM
    ON T.TagId = TM.TagId Inner Join Media M
    ON TM.MediaId = M.MediaId
    Where T.TagId = @TagId AND M.UserId = @UserId
   
    
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Token_RetreiveCount]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Token_RetreiveCount 
	-- Add the parameters for the stored procedure here
(
	@Token nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) 
	From Token
	Where Token.Token = @Token AND Token.IsValid = 1
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[UploadBatch_GetById]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UploadBatch_GetById
	-- Add the parameters for the stored procedure here
(
	@BatchId uniqueidentifier
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * 
	From UploadBatch
	Where UploadBatch.BatchId = @BatchId
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[UploadBatch_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UploadBatch_Insert]
	-- Add the parameters for the stored procedure here
(
	@LocationName nvarchar(100) = null,
	@Latitude decimal(18, 12) = null,
	@Longitude decimal(18, 12) = null,
	@Zoom int = null,
	@MapTypeId nvarchar(50) = null,
	@BatchId uniqueidentifier,
	@Year int = null,
	@Month int = null,
	@Day int = null,
	@Albums nvarchar(150) = null,
	@Tags nvarchar(500) = null,
	@MediaStatus nvarchar(50) = null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into UploadBatch (LocationName, Latitude, Longitude, Zoom, 
	MapTypeId, BatchId, [Year], [Month], [Day], Albums, Tags, MediaStatus)
	Values (@LocationName, @Latitude, @Longitude, @Zoom, 
	@MapTypeId, @BatchId, @Year, @Month, @Day, @Albums, @Tags, @MediaStatus)
	
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[UploaderMedia_SelectAllAndSetUploadStatusToPending]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[UploaderMedia_SelectAllAndSetUploadStatusToPending]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @QueuedMediaItems TABLE
	(
		MediaId int,
		UploadStatus nvarchar(50),
		UserId int
	)
	
	Insert Into @QueuedMediaItems(MediaId, UploadStatus, UserId)
	Select MediaId, UploadStatus, UserId From Media Where UploadStatus = 'Queued'
	
	Update Media
	Set UploadStatus = 'Pending'
	Where UploadStatus = 'Queued'
	
	Select MediaId, UploadStatus, UserId From @QueuedMediaItems


END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[UploaderMedia_SelectByPrimaryKey]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[UploaderMedia_SelectByPrimaryKey]
( 
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From Media
	Where MediaId = @MediaId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[UploaderMedia_UpdateStatus]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UploaderMedia_UpdateStatus]
	-- Add the parameters for the stored procedure here
(
	@MediaId int,
	@Status nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Media 
	Set [UploadStatus] = @Status
	Where MediaId = @MediaId
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_CheckAvailability]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE User_CheckAvailability
	-- Add the parameters for the stored procedure here
(
	@Username nvarchar(40)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select COUNT(*) as [Count] 
	From [User]
	Where Username = @Username
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_Delete]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  User_Delete
( 
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From [User]
	Where UserId = @UserId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_Insert]
( 
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Password nvarchar(250),
	@Email nvarchar(250),
	@DisplayName nvarchar(100),
	@Deleted bit,
	@Username nvarchar(50),
	@AccountStatus nvarchar(50) = 'Public',
	@Identity int OUTPUT
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[User] (FirstName, LastName, Password, Email, CreateDate, DisplayName, Deleted, Username, CurrentSession, AccountStatus) 
	VALUES (@FirstName, @LastName, @Password, @Email, GETUTCDATE(), @DisplayName, @Deleted, @Username, GETUTCDATE(), @AccountStatus)
	
	set @Identity = (select @@IDENTITY)
	return @Identity

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_Update]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_Update]
( 
	@UserId int,
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Password nvarchar(250),
	@Email nvarchar(250),
	@DisplayName nvarchar(100),
	@Deleted bit,
	@Username nvarchar(50),
	@AccountStatus nvarchar(50),
	@EnableReceivingOfEmails bit,
	@WebViewMaxHeight smallint , 
	@WebViewMaxWidth smallint
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update [User]
	SET 
	FirstName = @FirstName,
	LastName = @LastName,
	[Password] = @Password,
	Email = @Email,
	DisplayName = @DisplayName,
	Deleted = @Deleted,
	Username = @Username,
	AccountStatus = @AccountStatus,
	EnableReceivingOfEmails = @EnableReceivingOfEmails,
	WebViewMaxHeight = @WebViewMaxHeight, 
	WebViewMaxWidth = @WebViewMaxWidth
	
 
	Where UserId = @UserId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[WaitingList_Insert]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE WaitingList_Insert 
	-- Add the parameters for the stored procedure here
(
	@EmailAddress nvarchar(150)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO WaitingList(EmailAddress)Values(@EmailAddress)
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[AlbumView]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.AlbumView
AS
SELECT     A.AlbumId, A.Name, A.Description, A.UserId, A.ParentId, A.CoverMediaId, COALESCE (C.AlbumCount, 0) AS ChildAlbumCount, COALESCE (AM.PhotoCount, 0) 
                      AS PhotoCount
FROM         dbo.Album AS A LEFT OUTER JOIN
                          (SELECT     COUNT(AlbumId) AS AlbumCount, ParentId
                            FROM          dbo.Album
                            GROUP BY ParentId) AS C ON A.AlbumId = C.ParentId LEFT OUTER JOIN
                          (SELECT     COUNT(AlbumId) AS PhotoCount, AlbumId
                            FROM          dbo.AlbumMedia
                            GROUP BY AlbumId) AS AM ON A.AlbumId = AM.AlbumId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[CommentView]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.CommentView
AS
SELECT     dbo.Comment.CommentId, dbo.Comment.Name, dbo.Comment.Email, dbo.Comment.SiteUrl, dbo.Comment.Ip, dbo.Comment.UserAgent, dbo.Comment.CommentStatus, 
                      dbo.Comment.Text, dbo.Comment.CommentDate, dbo.Comment.UserId, dbo.Comment.ParentId, dbo.Media.UserId AS MediaUserId, dbo.Comment.MediaId
FROM         dbo.Comment INNER JOIN
                      dbo.Media ON dbo.Comment.MediaId = dbo.Media.MediaId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[CurrentMediaDetailsView]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.CurrentMediaDetailsView
AS
SELECT     TOP (1) Title, Description, Tags, MediaYear, MediaDay, MediaMonth, MediaId, InsertDate, UserId
FROM         dbo.MediaLedger
ORDER BY InsertDate
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[FileView]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.FileView
AS
SELECT     dbo.MediaFile.MediaType, dbo.MediaFile.MediaFormat, dbo.[File].OriginalFileName, dbo.[File].FileExtension, dbo.[File].FilePath, dbo.[File].FileId, 
                      dbo.MediaFile.MediaId
FROM         dbo.[File] INNER JOIN
                      dbo.MediaFile ON dbo.[File].FileId = dbo.MediaFile.FileId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[FriendView]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.FriendView
AS
SELECT     U.FirstName, U.LastName, U.Email, U.DisplayName, U.Username, dbo.Friend.UserId, dbo.Friend.FriendId,
                          (SELECT     COUNT(Media.MediaId)
                            FROM          Media
                            WHERE      UserId = U.UserId) AS PhotoCount,
                         COALESCE(  (SELECT     TOP (1) ROW_NUMBER() OVER (ORDER BY TagId ASC) AS C
FROM         (SELECT     TagMedia.MediaId, TM.TagId
                       FROM          (SELECT     TagId
                                               FROM          TagMedia
                                               GROUP BY TagId) TM INNER JOIN
                                              TagMedia ON TM.TagId = TagMedia.TagId INNER JOIN
                                              Media ON TagMedia.MediaId = Media.MediaId
                       WHERE      UserId = U.UserId) E
GROUP BY TagId
ORDER BY C DESC), 0 ) AS TagCount,
    (SELECT     COUNT(*) AS C
      FROM          Friend
      WHERE      UserId = U.UserId) AS FriendCount
FROM         [User] U INNER JOIN
                      dbo.Friend ON U.UserId = dbo.Friend.FriendId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaView]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.MediaView
AS
SELECT     dbo.Media.Status, dbo.Media.CreateDate, dbo.Media.UserId, dbo.Media.MediaId, dbo.[User].FirstName, dbo.[User].AccountStatus, dbo.[User].LastName, 
                      dbo.[User].Deleted, dbo.[User].Password, dbo.[User].Email, dbo.[User].DisplayName, dbo.[User].Username,  
                      dbo.[User].EnableReceivingOfEmails, dbo.[User].WebViewMaxWidth, dbo.[User].WebViewMaxHeight, L.Title, L.Description,  
                      SUBSTRING(
						(SELECT ', ' + T.TagName
						FROM Tag T
						Inner Join TagMedia TM
							On T.TagId = TM.TagId
						Where TM.MediaId = L.MediaId
						ORDER BY T.TagName
						FOR XML PATH('')),2,200000) as Tags, 
						L.MediaDay, L.MediaYear, L.MediaMonth,
                          (SELECT     COUNT(*) AS CommentCount
                            FROM          Comment
                            WHERE      Comment.MediaId = dbo.Media.MediaId) AS CommentCount
FROM         dbo.Media INNER JOIN
                      dbo.[User] ON dbo.Media.UserId = dbo.[User].UserId INNER JOIN
                          (SELECT     MediaId, Title, Description, Tags, MediaDay, MediaYear, MediaMonth
                            FROM          (SELECT     MediaId, InsertDate, Title, Description, Tags, MediaDay, MediaYear, MediaMonth, row_number() OVER (partition BY MEdiaID
                                                    ORDER BY InsertDate DESC) AS rn
                            FROM          dbo.MediaLedger) AS T
WHERE     rn = 1) AS L ON Media.MediaId = L.MediaId
WHERE     dbo.Media.UploadStatus = 'Completed'
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaViewAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.MediaViewAll
AS
SELECT     dbo.Media.Status, dbo.Media.CreateDate, dbo.Media.UserId, dbo.Media.MediaId, dbo.[User].FirstName, dbo.[User].AccountStatus, dbo.[User].LastName, 
                      dbo.[User].Deleted, dbo.[User].Password, dbo.[User].Email, dbo.[User].DisplayName, dbo.[User].Username, 
                      dbo.[User].EnableReceivingOfEmails, L.Title, L.Description, SUBSTRING(
						(SELECT ', ' + T.TagName
						FROM Tag T
						Inner Join TagMedia TM
							On T.TagId = TM.TagId
						Where TM.MediaId = L.MediaId
						ORDER BY T.TagName
						FOR XML PATH('')),2,200000) as Tags,  L.MediaDay, L.MediaYear, L.MediaMonth,
                          (SELECT     COUNT(*) AS CommentCount
                            FROM          Comment
                            WHERE      Comment.MediaId = dbo.Media.MediaId) AS CommentCount
FROM         dbo.Media INNER JOIN
                      dbo.[User] ON dbo.Media.UserId = dbo.[User].UserId INNER JOIN
                          (SELECT     MediaId, Title, Description, Tags, MediaDay, MediaYear, MediaMonth
                            FROM          (SELECT     MediaId, InsertDate, Title, Description, Tags, MediaDay, MediaYear, MediaMonth, row_number() OVER (partition BY MEdiaID
                                                    ORDER BY InsertDate DESC) AS rn
                            FROM          dbo.MediaLedger) AS T
WHERE     rn = 1) AS L ON Media.MediaId = L.MediaId
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[UserView]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE VIEW dbo.UserView
AS
SELECT     UserId, FirstName, LastName, Password, Email, CreateDate, DisplayName, Deleted, Username, CurrentSession, LastLogin, AccountStatus, EnableReceivingOfEmails, 
                      WebViewMaxHeight, WebViewMaxWidth
FROM         dbo.[User]
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_RetrieveAllByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveAllByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From AlbumView
	Where UserId = @UserId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_RetrieveAllByUserIdAndParentId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveAllByUserIdAndParentId]
(
	@UserId int, 
	@ParentId int = null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Declare @UserId int 
	--Declare @ParentId int 
	--set @UserId = 9
	--set @ParentId = 5

	Select * 
	From AlbumView
	Where UserId = @UserId AND COALESCE(ParentId, 0) = COALESCE(@ParentId, 0)   
	ORder by Name asc
	
	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_RetrieveByPrimaryKeyAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveByPrimaryKeyAndUserId]
( 
	@AlbumId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From AlbumView
	Where AlbumId = @AlbumId AND UserId = @UserId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_RetrieveTopLevelAlbumsByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveTopLevelAlbumsByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From AlbumView A
	Where UserId = @UserId AND COALESCE(ParentId, -1) = -1
	Order By A.Name asc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_Search]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Search]
(
	@UserId int,
	@AlbumId int = null,
	@SearchText nvarchar(50) = null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	IF @AlbumId is NOT NULL
	BEGIN
		
	WITH Hierarchy(AlbumID, Name, ParentID, Description, UserId, PhotoCount, AlbumCount, HLevel)
	AS
	(
		SELECT AlbumID, Name, ParentID, Description, UserId, PhotoCount, ChildAlbumCount, 0 as HLevel
		FROM AlbumView 
		WHERE (AlbumID = @AlbumId AND UserId = @UserId)

		UNION ALL

		SELECT SubAlbum.AlbumID, SubAlbum.Name, SubAlbum.ParentID, SubAlbum.Description, SubAlbum.UserId, SubAlbum.PhotoCount, SubAlbum.ChildAlbumCount, HLevel + 1
		FROM AlbumView SubAlbum
		INNER JOIN Hierarchy ParentDepartment ON 
		SubAlbum.ParentId = ParentDepartment.AlbumID 
	)

	SELECT A.AlbumID, A.Name, A.ParentID, A.Description, A.UserId, HLevel, 0 as ChildAlbumCount,0 as PhotoCount
	FROM  Hierarchy Inner Join Album A ON
	Hierarchy.AlbumID = A.AlbumId
	Where A.Name Like '%' + @SearchText + '%' OR A.[Description] Like '%' + @SearchText + '%'

	ORDER BY A.Name DESC

	END
	ELSE
	BEGIN

	SELECT A.AlbumID, A.Name, A.ParentID, A.Description, A.UserId, ChildAlbumCount,PhotoCount
	FROM  AlbumView A
	Where A.UserId = @UserId AND A.Name Like '%' + @SearchText + '%' OR A.[Description] Like '%' + @SearchText + '%'

	END

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_SelectAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_SelectAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From AlbumView

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Album_SelectByPrimaryKey]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_SelectByPrimaryKey]
( 
	@AlbumId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From AlbumView
	Where AlbumId = @AlbumId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Comment_RetrieveCommentByStatusAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveCommentByStatusAndUserId]
(
	@UserId int,
	@CommentStatus nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From CommentView
	Where CommentStatus = @CommentStatus
	And MediaUserId = @UserId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[File_RetrieveByMediaId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE File_RetrieveByMediaId 
	-- Add the parameters for the stored procedure here
(
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  * FROM FileView where MediaId = @MediaId
                      
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[File_RetrieveByMediaIdCollection]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[File_RetrieveByMediaIdCollection] 
	-- Add the parameters for the stored procedure here
(
	@Ids IdCollection ReadOnly
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  * FROM FileView FV
	Inner join @Ids I
		ON  I.Id = FV.MediaId --  where MediaId = @MediaId
                      
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Friend_RetrieveFriendById]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 28, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Friend_RetrieveFriendById]
(
	@FriendId int, 
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From FriendView
	Where FriendId = @FriendId AND UserId = @UserId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Friend_RetrievesFriendByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 28, 2009
-- =============================================
Create PROCEDURE  [dbo].[Friend_RetrievesFriendByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From FriendView
	Where UserId = @UserId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetreiveMediaStatusesCountByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetreiveMediaStatusesCountByUserId]
(
	@UserID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select StatusCount, Status From
		(
			Select COUNT(Status) as StatusCount, Status, UserId
			From MediaView 
			GRoup By Status, UserId
		) T
	Where T.UserId = @UserID
	
	--Union ALL
	
	--Select COUNT(*)as StatusCount,  null as Status
	--From MediaView
	--Where UserId = @UserID
	
	--Union All
	
	--Select COUNT(*)as StatusCount, 'Untagged' as Status
	--From MediaView
	--Where [Status] = 'UnPublished' AND LEN(Tags) = 0 AND UserId = @UserID
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_Retrieve25RecentPhotosByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Retrieve25RecentPhotosByUserId]
(
	@UserId int,
	@Visibility nvarchar(50) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(50) *
	From MediaView 
	Where UserId = @UserId AND MediaView.Status = @Visibility
	Order by CreateDate desc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_Retrieve33RecentPhotosByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Retrieve33RecentPhotosByUserId]
(
	@UserId int,
	@Visibility nvarchar(50) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(17) *
	From MediaView 
	Where UserId = @UserId AND MediaView.Status = @Visibility
	Order by CreateDate desc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveByAlbumIdAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveByAlbumIdAndUserId]
( 
	@AlbumId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView Inner Join AlbumMedia ON
	MediaView.MediaId = AlbumMedia.MediaId Inner Join Album ON
	Album.AlbumId = AlbumMedia.AlbumId	
	Where Album.AlbumId = @AlbumId AND MediaView.UserId = @UserId
	Order By CASE WHEN AlbumMedia.Position IS NULL THEN 1 ELSE 0 END, AlbumMedia.Position
	--Order By AlbumMedia.Position asc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveByStatus]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveByStatus]
(
	@Status nvarchar(50),
	@userId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView
	Where [Status] = @Status AND UserId = @userId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveByTagIdAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Media_RetrieveByTagIdAndUserId]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@TagId int,
	@Visiblity nvarchar(50) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   * 
	From MediaView INNER JOIN
                      dbo.TagMedia ON MediaView.MediaId = dbo.TagMedia.MediaId INNER JOIN
                      dbo.Tag ON dbo.TagMedia.TagId = dbo.Tag.TagId
    Where dbo.TagMedia.TagId = @TagId AND MediaView.UserId = @UserId AND MediaView.Status = @Visiblity
    Order by TagName asc
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveByTagNameAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Media_RetrieveByTagNameAndUserId]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@TagName nvarchar(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   * 
	From MediaView INNER JOIN
                      dbo.TagMedia ON MediaView.MediaId = dbo.TagMedia.MediaId INNER JOIN
                      dbo.Tag ON dbo.TagMedia.TagId = dbo.Tag.TagId
    Where TagName = @TagName AND MediaView.UserId = @UserId
    Order by TagName asc
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveByUserId]
( 
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView 
	Where UserId = @UserId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]
( 
	@AlbumId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Select  MediaView.MediaId
	--From  
	--(
	--	Select AlbumMedia.MediaId, AlbumMedia.AlbumId From
	--	(	
	--	) D Inner Join AlbumMedia ON
	--	AlbumMedia.MediaId <> D.MediaId
	--) T  Inner Join MediaView ON
	--T.MediaID = MediaView.MediaId
	
	Select * From 
	(	
		Select AM.MediaId as AlbumMediaId
		From AlbumMedia AM Inner Join Album A
		ON AM.AlbumId = A.AlbumId
		Where A.UserId = @UserId AND A.AlbumId = @AlbumId
		Group By AM.MediaId	
	) AM right Join MediaView MV
	ON AM.AlbumMediaId = MV.MediaId 
	Where AlbumMediaId is Null AND MV.UserId = @UserId
	
	
	--Select AlbumMedia.MediaId, AlbumMedia.AlbumId From MediaView inner Join AlbumMedia On
	--		MediaView.MediaId <> AlbumMedia.MediaId
	--		Group By AlbumMedia.MediaId, AlbumMedia.AlbumId
	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveRandom50Photos]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveRandom50Photos]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(84) *
	From MediaView 
	Order by NEWID() asc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveRandom84PhotosByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveRandom84PhotosByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(100) *
	From MediaView 
	Where UserId = @UserId
	Order by NEWID() asc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveRandom90Photos]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[Media_RetrieveRandom90Photos]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(90) *
	From MediaView 
	Where Status = 'public'
	Order by NEWID() asc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_RetrieveRandomByAlbumId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveRandomByAlbumId]
( 
	@AlbumId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(1) *
	From MediaView Inner Join AlbumMedia ON
	MediaView.MediaId = AlbumMedia.MediaId Inner Join Album ON
	Album.AlbumId = AlbumMedia.AlbumId	
	Where Album.AlbumId = @AlbumId AND MediaView.UserId = @UserId
	Order by NEWID() asc

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_Search]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Media_Search
	-- Add the parameters for the stored procedure here
(
	@Search nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * From MediaView
	Where Title LIKE '%' + @Search + '%' OR  [Description] LIKE '%' + @Search + '%' OR Tags LIKE '%' + @Search + '%'
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_SearchByTextAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Media_SearchByTextAndUserId]
	-- Add the parameters for the stored procedure here
(
	@Search nvarchar(50),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * From MediaView
	Where UserId = @UserId AND (Title LIKE '%' + @Search + '%' OR  [Description] LIKE '%' + @Search + '%' OR Tags LIKE '%' + @Search + '%')
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_SearchPhotosThatDoNotIncludePhotosByAlbumId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_SearchPhotosThatDoNotIncludePhotosByAlbumId]
( 
	@AlbumId int,
	@UserId int,
	@Search nvarchar(150)
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	
	Select * From 
	(	
		Select AM.MediaId as AlbumMediaId
		From AlbumMedia AM Inner Join Album A
		ON AM.AlbumId = A.AlbumId
		Where A.UserId = @UserId AND A.AlbumId = @AlbumId
		Group By AM.MediaId	
	) AM right Join MediaView MV
	ON AM.AlbumMediaId = MV.MediaId 
	Where AlbumMediaId is Null  AND  (@Search is Not Null AND Len(@Search) > 0) 
	AND MV.UserId = @UserId 
	AND (Title LIKE '%' + @Search + '%' OR  [Description] LIKE '%' + @Search + '%' OR Tags LIKE '%' + @Search + '%')
	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_SearchTagsForPhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_SearchTagsForPhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]
( 
	@AlbumId int,
	@UserId int,
	@Tags  Tags READONLY
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	--Select * From 
	--(	
	--	Select AM.MediaId as AlbumMediaId
	--	From AlbumMedia AM Inner Join Album A
	--	ON AM.AlbumId = A.AlbumId
	--	Where A.UserId = @UserId AND A.AlbumId = @AlbumId
	--	Group By AM.MediaId	
	--) AM right Join MediaView MV
	--ON AM.AlbumMediaId = MV.MediaId 
	--Where AlbumMediaId is Null 
	--AND MV.UserId = @UserId 
	--AND ( Tags LIKE '%' + @Tag + '%')
	
	Select *
	From TagMedia TM
	Inner Join (
				Select TagId
				From Tag
				Inner Join @Tags NT
					On Tag.TagName = NT.Tag
	
				) T
		On TM.TagId = T.TagId
	Inner Join MediaView M
		ON TM.MediaId = M.MediaId
	Where M.UserId = @UserId
	AND M.MediaId Not IN
	(
		Select MediaId
		From AlbumMedia A
		Where A.AlbumId = @AlbumId
	)
	
	--Select  TM. 
	--From Tag 
	--Inner Join 
	--	(Select TagId, UserId, COUNT(TagId) as TagCount 
	--	 From TagMedia Inner Join Media On 
	--			TagMedia.MediaId = Media.MediaId
	--			Group By TagId, UserId
	--	) TM ON		
	--Tag.TagId = TM.TagId
	--Inner Join (
	--			 Select TagName, TagId 
	--			 From Tag T
	--			 Inner Join @Tags NT
	--				ON T.TagName = NT.Tag				 		
	--			) T
	--ON TM.TagId = T.TagId			 
	--Where UserId = @UserId 
	--Order by T.TagName asc
	

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_SelectAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_SelectAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_SelectByPrimaryKey]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_SelectByPrimaryKey]
( 
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView 
	Where MediaId = @MediaId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Media_SelectByPrimaryKeyAndUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[Media_SelectByPrimaryKeyAndUserId]
( 
	@MediaId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView 
	Where MediaId = @MediaId AND UserId = @UserId

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaUploadBatch_RetrieveMediaByBatchId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaUploadBatch_RetrieveMediaByBatchId]
(
	@BatchId uniqueidentifier
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * 
	From MediaUploadBatch MB Inner join MediaViewAll MV
	ON  MB.MediaId = MV.MediaId
	Where MB.UploadBatch = @BatchId
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Recent_GetYearByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Recent_GetYearByUserId
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@Year int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select M.*
	From (Select	MediaId,
			Title,
			Description,
			Tags, 
			MediaDay, 
			MediaYear, 
			MediaMonth
			
	FROM          
			(SELECT MediaId, 
					InsertDate, 
					Title, 
					Description, 
					Tags, 
					MediaDay, 
					MediaYear,
					MediaMonth, 
					row_number() OVER	(
											partition BY MEdiaID 
											ORDER BY InsertDate DESC			
										) AS rn
					FROM dbo.MediaLedger
		    ) AS T
	WHERE rn = 1) E 
	Inner Join MediaView M
		ON E.MediaId = M.MediaId
	Where E.MediaYear = @Year AND M.UserId = @UserId
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Recent_Uploads]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  Recent_Uploads
	-- Add the parameters for the stored procedure here
(
	@photoCount int,
	@UserId int,
	@Status nvarchar(20) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	Create TABLE #TopResults 
	(
		  MediaId int,
		  CreateDate datetime
	)
	
	Create TABLE #RecentUploads 
	(
		  MediaId int,
		  Age nvarchar(100)
	)
	
    -- Insert statements for procedure here
	Declare @Sql nvarchar(200); 
	set @Sql = 'Select top(' + Cast(@photoCount as nvarchar) + ') MediaId, CreateDate 
				From MediaView
				Where UserId = ' + CAST(@UserId as nvarchar) + ' AND [Status] = ''' + @Status + '''
				Order by CreateDate desc'
	
	Insert into #TopResults (MediaId, CreateDate)
	EXEC SP_EXECUTESQL @Sql		
		
	-- Day 1
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'DayOne' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(HOUR , -24, GETUTCDATE() )
	
	-- Day 2
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'DayTwo' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(HOUR , -72, GETUTCDATE())
	AND CreateDate < DATEADD(HOUR , -24, GETUTCDATE())
	
	-- Week 1
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'WeekOne' as Age 
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -14, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -3, GETUTCDATE())

	-- Week 2
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'WeekTwo' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -21, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -14, GETUTCDATE())
	
	-- Week 3
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'WeekThree' as Age 
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -27, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -21, GETUTCDATE())
	
	-- Month 1
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthOne' as Age 
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -59, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -27, GETUTCDATE())
	
	-- Month 2
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthTwo' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -89, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -59, GETUTCDATE())
	
	-- Month 3
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthThree' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -119, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -89, GETUTCDATE())
	
	-- Month 4
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthFour' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -149, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -119, GETUTCDATE())
	
	-- Month 5
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthFive' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -179, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -149, GETUTCDATE())
	
	-- Month 6
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthSix' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -209, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -179, GETUTCDATE())
	
	-- Older
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'Older' as Age
	From #TopResults T 
	Where CreateDate < DATEADD(DAY , -209, GETUTCDATE())

	Select M.*, U.Age 
	from #RecentUploads U
	Inner Join MediaView M
		On U.MediaId = M.MediaId
	
		
	Drop Table #RecentUploads
	Drop Table #TopResults 
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[RecentActivity_RetrieveFriendsActivityFromLastWeekByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecentActivity_RetrieveFriendsActivityFromLastWeekByUserId] 
	-- Add the parameters for the stored procedure here
(
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Select * 
	--	From RecentActivity RA Inner Join FriendView FV
	--On  RA.UserId = FV.UserId
	--Where FV.UserId = 9 AND CreateDate > DATEADD(DAY , -7, GETUTCDATE())


    -- Insert statements for procedure here
	Select ActivityType, SUM(ActivityCount) as ActivityCount , FriendId
	From RecentActivity RA Inner Join FriendView FV
	On  RA.UserId = FV.UserId
	Where FV.UserId = @UserId AND CreateDate > DATEADD(DAY , -7, GETUTCDATE())
	Group By  ActivityType, FriendId
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Reporting_Top10MostViewed]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Reporting_Top10MostViewed
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Top 5 * 
	From MediaViewed MV Inner Join MediaView M
	ON MV.MediaId = M.MediaId
	Where M.UserId = @UserId
	Order By ViewCount DESC
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[Tag_GetRelatedTagsByYear]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_GetRelatedTagsByYear]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@Year int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	Select M.MediaId
	Into #Year
	From (Select	MediaId,
			Title,
			Description,
			Tags, 
			MediaDay, 
			MediaYear, 
			MediaMonth
			
	FROM          
			(SELECT MediaId, 
					InsertDate, 
					Title, 
					Description, 
					Tags, 
					MediaDay, 
					MediaYear,
					MediaMonth, 
					row_number() OVER	(
											partition BY MEdiaID 
											ORDER BY InsertDate DESC			
										) AS rn
					FROM dbo.MediaLedger
		    ) AS T
	WHERE rn = 1) E 
	Inner Join MediaView M
		ON E.MediaId = M.MediaId
	Where E.MediaYear = @Year AND M.UserId = @UserId
	
		Select T.*, 0 as TagCount 
	From (
		Select TM.TagId 
		From #Year M
		Inner Join TagMedia TM
			ON M.MediaId = TM.MediaId
		Group by TM.TagId
	  ) R
	Inner Join Tag T
	ON R.TagId = T.TagId
	
	Drop Table #Year
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_RetrieveByUsername]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_RetrieveByUsername]
( 
	@Username nvarchar(50)) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From UserView
	Where Username  = @Username

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_RetrieveRandomPhotoByUserId]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[User_RetrieveRandomPhotoByUserId]
(
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select Top(1) * 
	From MediaView M
	Where M.UserId = @UserId
	Order By NEWID()

	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_RetrieveUserAndMedia]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_RetrieveUserAndMedia]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select * From MediaView inner join 
								(
									Select UserId, (Select Top(1) MediaId From Media Where Status = 'Public' AND UserId = M.UserId ORder By NewId()) as MediaId  From Media as M Group By UserId
								) E On MediaView.MediaId = E.MediaId 
    

	
	
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_RetrieveUserByLoginCredentials]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_RetrieveUserByLoginCredentials]
	-- Add the parameters for the stored procedure here
	@Username nvarchar(50),
	@Password nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * 
	From UserView
	Where Username = @Username AND [Password] = @Password
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_SelectAll]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_SelectAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From UserView

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[User_SelectByPrimaryKey]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_SelectByPrimaryKey]
( 
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From UserView
	Where UserId = @UserId 

END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[CommentView].[MS_DiagramPane1]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Comment"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 298
               Right = 203
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Media"
            Begin Extent = 
               Top = 6
               Left = 241
               Bottom = 252
               Right = 401
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CommentView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[CommentView].[MS_DiagramPaneCount]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CommentView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[CurrentMediaDetailsView].[MS_DiagramPane1]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "MediaLedger"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 246
               Right = 391
            End
            DisplayFlags = 344
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CurrentMediaDetailsView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[CurrentMediaDetailsView].[MS_DiagramPaneCount]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CurrentMediaDetailsView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[FileView].[MS_DiagramPane1]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "File"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 188
               Right = 206
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MediaFile_1"
            Begin Extent = 
               Top = 6
               Left = 244
               Bottom = 205
               Right = 404
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'FileView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[FileView].[MS_DiagramPaneCount]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'FileView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[FriendView].[MS_DiagramPane1]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[8] 2[45] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'FriendView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[FriendView].[MS_DiagramPaneCount]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'FriendView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaView].[MS_DiagramPane1]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[26] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MediaView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[MediaView].[MS_DiagramPaneCount]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MediaView';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Album] WITH CHECK CHECK CONSTRAINT [FK_Album_User];

ALTER TABLE [dbo].[AlbumMedia] WITH CHECK CHECK CONSTRAINT [FK_AlbumMedia_Album];

ALTER TABLE [dbo].[AlbumMedia] WITH CHECK CHECK CONSTRAINT [FK_AlbumMedia_Media];

ALTER TABLE [dbo].[Comment] WITH CHECK CHECK CONSTRAINT [FK_Comment_Media];

ALTER TABLE [dbo].[Exif] WITH CHECK CHECK CONSTRAINT [FK_Exif_Media];

ALTER TABLE [dbo].[Friend] WITH CHECK CHECK CONSTRAINT [FK_Friend_User];

ALTER TABLE [dbo].[Friend] WITH CHECK CHECK CONSTRAINT [FK_Friend_User1];

ALTER TABLE [dbo].[Invitation] WITH CHECK CHECK CONSTRAINT [FK_Invitation_User];

ALTER TABLE [dbo].[MediaFile] WITH CHECK CHECK CONSTRAINT [FK_MediaFile_File];

ALTER TABLE [dbo].[MediaFile] WITH CHECK CHECK CONSTRAINT [FK_MediaFile_Media];

ALTER TABLE [dbo].[TagMedia] WITH CHECK CHECK CONSTRAINT [FK_TagMedia_Media];

ALTER TABLE [dbo].[TagMedia] WITH CHECK CHECK CONSTRAINT [FK_TagMedia_Tag];

ALTER TABLE [dbo].[Viewed] WITH CHECK CHECK CONSTRAINT [FK_Viewed_Media];

ALTER TABLE [dbo].[Viewed] WITH CHECK CHECK CONSTRAINT [FK_Viewed_User];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
