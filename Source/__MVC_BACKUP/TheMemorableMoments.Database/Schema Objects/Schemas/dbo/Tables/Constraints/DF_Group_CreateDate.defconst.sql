ALTER TABLE [dbo].[Group]
    ADD CONSTRAINT [DF_Group_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

