CREATE TABLE [dbo].[User] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50)  NOT NULL,
    [LastName]   NVARCHAR (50)  NOT NULL,
    [Username]   NVARCHAR (50)  NOT NULL,
    [Email]      NVARCHAR (250) NOT NULL,
    [Password]   NVARCHAR (250) NOT NULL,
    [CreateDate] DATETIME2 (7)  NOT NULL
);

