ALTER TABLE [dbo].[MediaViewed]
    ADD CONSTRAINT [DF_MediaViewed_ViewCount] DEFAULT ((1)) FOR [ViewCount];

