﻿ALTER TABLE [dbo].[Friend]
    ADD CONSTRAINT [FK_Friend_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

