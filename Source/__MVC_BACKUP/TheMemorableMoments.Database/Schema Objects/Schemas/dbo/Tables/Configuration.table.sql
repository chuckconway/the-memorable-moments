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



