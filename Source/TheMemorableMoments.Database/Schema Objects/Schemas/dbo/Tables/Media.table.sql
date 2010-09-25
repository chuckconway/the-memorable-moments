CREATE TABLE [dbo].[Media] (
    [MediaId]      INT           IDENTITY (1, 1) NOT NULL,
    [UserId]       INT           NOT NULL,
    [Status]       NVARCHAR (50) NOT NULL,
    [CreateDate]   DATETIME2 (7) NOT NULL,
    [UploadStatus] NVARCHAR (50) NOT NULL
);



