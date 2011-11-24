CREATE TABLE [dbo].[MediaViewed] (
    [MediaViewedId] INT           IDENTITY (1, 1) NOT NULL,
    [UserId]        INT           NOT NULL,
    [MediaId]       INT           NOT NULL,
    [LastViewed]    DATETIME2 (7) NOT NULL,
    [ViewCount]     INT           NOT NULL,
    [FirstViewed]   DATETIME2 (7) NOT NULL
);



