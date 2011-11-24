ALTER TABLE [dbo].[MediaFile]
    ADD CONSTRAINT [FK_MediaFile_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

