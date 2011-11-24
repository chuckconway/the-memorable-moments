CREATE TABLE [dbo].[MediaQueue] (
    [MediaQueueId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [MediaId]      INT             NOT NULL,
    [MediaBytes]   VARBINARY (MAX) NOT NULL,
    [Filename]     NVARCHAR (250)  NOT NULL
);



