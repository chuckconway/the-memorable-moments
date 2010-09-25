CREATE TABLE [dbo].[File] (
    [FileId]           INT            IDENTITY (1, 1) NOT NULL,
    [FilePath]         NVARCHAR (255) NOT NULL,
    [FileExtension]    NVARCHAR (10)  NOT NULL,
    [OriginalFileName] NVARCHAR (200) NOT NULL,
    [CreateDate]       DATETIME2 (7)  NOT NULL,
    [Size]             BIGINT         NOT NULL,
    [Width]            INT            NOT NULL,
    [Height]           INT            NOT NULL
);





