ALTER TABLE [dbo].[Viewed]
    ADD CONSTRAINT [FK_Viewed_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

