CREATE TABLE [dbo].[User] (
    [UserId]                  INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]               NVARCHAR (100) NOT NULL,
    [LastName]                NVARCHAR (100) NOT NULL,
    [Password]                NVARCHAR (250) NOT NULL,
    [Email]                   NVARCHAR (250) NOT NULL,
    [CreateDate]              DATETIME2 (7)  NOT NULL,
    [DisplayName]             NVARCHAR (100) NULL,
    [Deleted]                 BIT            NOT NULL,
    [Username]                NVARCHAR (50)  NOT NULL,
    [LastLogin]               DATETIME2 (7)  NULL,
    [CurrentSession]          DATETIME2 (7)  NULL,
    [AccountStatus]           NVARCHAR (50)  NULL,
    [EnableReceivingOfEmails] BIT            NOT NULL,
    [WebViewMaxWidth]         SMALLINT       NOT NULL,
    [WebViewMaxHeight]        SMALLINT       NOT NULL,
    [MaxInvitations]          TINYINT        NOT NULL
);











