ALTER TABLE [dbo].[PersistentCollection]
    ADD CONSTRAINT [DF_DynamicCollection_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

