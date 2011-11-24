CREATE TABLE [dbo].[Location] (
    [LocationName] NVARCHAR (250)   NOT NULL,
    [Latitude]     NUMERIC (26, 13) NOT NULL,
    [Longitude]    NUMERIC (26, 13) NOT NULL,
    [UserId]       INT              NOT NULL,
    [Zoom]         TINYINT          NOT NULL,
    [MapTypeId]    NVARCHAR (50)    NOT NULL
);





