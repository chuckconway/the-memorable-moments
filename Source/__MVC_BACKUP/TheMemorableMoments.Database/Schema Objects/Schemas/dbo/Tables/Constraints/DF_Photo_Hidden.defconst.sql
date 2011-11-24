ALTER TABLE [dbo].[Media]
    ADD CONSTRAINT [DF_Photo_Hidden] DEFAULT ((1)) FOR [Status];

