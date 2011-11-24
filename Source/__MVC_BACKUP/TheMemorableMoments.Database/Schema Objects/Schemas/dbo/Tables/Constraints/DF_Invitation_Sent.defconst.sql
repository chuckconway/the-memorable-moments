ALTER TABLE [dbo].[Invitation]
    ADD CONSTRAINT [DF_Invitation_Sent] DEFAULT ((0)) FOR [Sent];

