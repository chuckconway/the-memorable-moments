ALTER TABLE [dbo].[Friend]
    ADD CONSTRAINT [DF_Friend_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

