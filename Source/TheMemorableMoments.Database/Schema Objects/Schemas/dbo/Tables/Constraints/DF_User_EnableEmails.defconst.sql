﻿ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_EnableEmails] DEFAULT ((1)) FOR [EnableReceivingOfEmails];

