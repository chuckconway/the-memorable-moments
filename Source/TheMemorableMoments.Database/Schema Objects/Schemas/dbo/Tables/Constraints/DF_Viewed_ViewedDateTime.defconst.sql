ALTER TABLE [dbo].[Viewed]
    ADD CONSTRAINT [DF_Viewed_ViewedDateTime] DEFAULT (getutcdate()) FOR [ViewedDateTime];

