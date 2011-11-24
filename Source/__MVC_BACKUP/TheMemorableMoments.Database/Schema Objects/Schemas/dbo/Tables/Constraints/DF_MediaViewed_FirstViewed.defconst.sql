ALTER TABLE [dbo].[MediaViewed]
    ADD CONSTRAINT [DF_MediaViewed_FirstViewed] DEFAULT (getutcdate()) FOR [FirstViewed];

