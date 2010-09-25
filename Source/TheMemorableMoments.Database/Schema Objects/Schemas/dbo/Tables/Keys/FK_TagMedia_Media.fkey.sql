ALTER TABLE [dbo].[TagMedia]
    ADD CONSTRAINT [FK_TagMedia_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

