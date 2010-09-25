ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_MaxInvitation] DEFAULT ((5)) FOR [MaxInvitations];

