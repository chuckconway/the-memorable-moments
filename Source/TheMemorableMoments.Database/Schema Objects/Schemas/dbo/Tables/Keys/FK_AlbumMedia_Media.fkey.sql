ALTER TABLE [dbo].[AlbumMedia]
    ADD CONSTRAINT [FK_AlbumMedia_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

