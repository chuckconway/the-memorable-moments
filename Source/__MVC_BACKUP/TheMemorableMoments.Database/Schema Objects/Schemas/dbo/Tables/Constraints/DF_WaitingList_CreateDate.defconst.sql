ALTER TABLE [dbo].[WaitingList]
    ADD CONSTRAINT [DF_WaitingList_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

