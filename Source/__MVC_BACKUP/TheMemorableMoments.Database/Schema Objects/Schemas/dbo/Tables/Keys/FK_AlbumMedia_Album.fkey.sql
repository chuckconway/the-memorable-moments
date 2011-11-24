ALTER TABLE [dbo].[AlbumMedia]
    ADD CONSTRAINT [FK_AlbumMedia_Album] FOREIGN KEY ([AlbumId]) REFERENCES [dbo].[Album] ([AlbumId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

