ALTER TABLE [dbo].[Email]
    ADD CONSTRAINT [DF_Email_SentDate] DEFAULT (getutcdate()) FOR [SentDate];

