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



