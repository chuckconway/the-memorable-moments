CREATE TABLE [dbo].[WaitingList] (
    [WaitingListId] INT            IDENTITY (1, 1) NOT NULL,
    [EmailAddress]  NVARCHAR (150) NOT NULL,
    [CreateDate]    DATETIME2 (7)  NOT NULL
);

