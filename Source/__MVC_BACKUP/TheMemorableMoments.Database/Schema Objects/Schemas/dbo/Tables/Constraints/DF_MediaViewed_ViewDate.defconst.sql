ALTER TABLE [dbo].[MediaViewed]
    ADD CONSTRAINT [DF_MediaViewed_ViewDate] DEFAULT (getutcdate()) FOR [LastViewed];



