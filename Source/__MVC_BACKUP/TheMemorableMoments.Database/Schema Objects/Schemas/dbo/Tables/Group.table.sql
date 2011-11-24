CREATE TABLE [dbo].[Group] (
    [GroupId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [CreateDate]  DATETIME2 (7)  NOT NULL,
    [CreatedBy]   INT            NOT NULL
);

