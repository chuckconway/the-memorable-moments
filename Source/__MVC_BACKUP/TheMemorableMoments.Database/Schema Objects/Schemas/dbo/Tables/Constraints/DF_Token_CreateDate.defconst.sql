ALTER TABLE [dbo].[Token]
    ADD CONSTRAINT [DF_Token_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

