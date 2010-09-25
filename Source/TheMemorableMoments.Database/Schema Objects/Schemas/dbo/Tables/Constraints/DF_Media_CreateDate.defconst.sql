ALTER TABLE [dbo].[Media]
    ADD CONSTRAINT [DF_Media_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

