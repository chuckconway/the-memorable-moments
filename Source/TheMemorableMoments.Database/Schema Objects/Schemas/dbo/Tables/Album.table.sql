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







