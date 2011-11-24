ALTER TABLE [dbo].[Comment]
    ADD CONSTRAINT [FK_Comment_Media] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([MediaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

