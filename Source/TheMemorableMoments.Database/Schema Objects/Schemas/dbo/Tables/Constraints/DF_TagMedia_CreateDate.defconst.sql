ALTER TABLE [dbo].[TagMedia]
    ADD CONSTRAINT [DF_TagMedia_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

