CREATE TABLE [dbo].[UploadBatch] (
    [BatchId]      UNIQUEIDENTIFIER NOT NULL,
    [LocationName] NVARCHAR (100)   NULL,
    [Latitude]     DECIMAL (18, 12) NULL,
    [Longitude]    DECIMAL (18, 12) NULL,
    [Zoom]         INT              NULL,
    [MapTypeId]    NVARCHAR (50)    NULL,
    [Year]         INT              NULL,
    [Month]        INT              NULL,
    [Day]          INT              NULL,
    [Albums]       NVARCHAR (150)   NULL,
    [Tags]         NVARCHAR (500)   NULL,
    [MediaStatus]  NVARCHAR (50)    NULL
);

