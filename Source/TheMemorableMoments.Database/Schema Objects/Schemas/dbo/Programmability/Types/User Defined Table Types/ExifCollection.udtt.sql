CREATE TYPE [dbo].[ExifCollection] AS  TABLE (
    [MediaId] INT             NOT NULL,
    [Key]     NVARCHAR (50)   NOT NULL,
    [Type]    INT             NOT NULL,
    [Value]   NVARCHAR (4000) NULL);



