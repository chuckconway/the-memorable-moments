ALTER TABLE [dbo].[Invitation]
    ADD CONSTRAINT [FK_Invitation_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

