CREATE TABLE [dbo].[Email] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [To]       NVARCHAR (250) NOT NULL,
    [From]     NVARCHAR (250) NOT NULL,
    [Subject]  NVARCHAR (500) NOT NULL,
    [Body]     NVARCHAR (MAX) NOT NULL,
    [UserId]   INT            NOT NULL,
    [SentDate] DATETIME2 (7)  NOT NULL
);

