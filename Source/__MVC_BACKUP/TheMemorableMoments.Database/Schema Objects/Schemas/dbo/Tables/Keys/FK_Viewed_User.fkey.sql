﻿ALTER TABLE [dbo].[Viewed]
    ADD CONSTRAINT [FK_Viewed_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

