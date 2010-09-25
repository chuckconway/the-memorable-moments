CREATE TABLE [dbo].[RecentActivity] (
    [RecentActivityId] INT           IDENTITY (1, 1) NOT NULL,
    [ActivityType]     NVARCHAR (50) NOT NULL,
    [ActivityCount]    INT           NOT NULL,
    [UserId]           INT           NOT NULL,
    [CreateDate]       DATETIME2 (7) NOT NULL
);



