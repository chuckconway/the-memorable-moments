CREATE TABLE [dbo].[MediaLedger] (
    [MediaLedgerId] INT             IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (100)  NULL,
    [Description]   NVARCHAR (2000) NULL,
    [Tags]          NVARCHAR (250)  NULL,
    [MediaYear]     INT             NULL,
    [MediaMonth]    INT             NULL,
    [MediaDay]      INT             NULL,
    [MediaId]       INT             NOT NULL,
    [InsertDate]    DATETIME2 (7)   NOT NULL,
    [UserId]        INT             NOT NULL,
    [LocationName]  NVARCHAR (250)  NULL
);











