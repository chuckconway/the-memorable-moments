﻿ALTER TABLE [dbo].[Album]
    ADD CONSTRAINT [FK_Album_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

