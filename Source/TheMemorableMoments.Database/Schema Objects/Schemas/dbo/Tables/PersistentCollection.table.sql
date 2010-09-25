CREATE TABLE [dbo].[PersistentCollection] (
    [CollectionKey] NVARCHAR (150)  NOT NULL,
    [Value]         NVARCHAR (1000) NOT NULL,
    [Persistence]   NVARCHAR (50)   NOT NULL,
    [CreateDate]    DATETIME2 (7)   NOT NULL
);

