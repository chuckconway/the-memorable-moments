ALTER TABLE [dbo].[Invitation]
    ADD CONSTRAINT [DF_Invitation_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

