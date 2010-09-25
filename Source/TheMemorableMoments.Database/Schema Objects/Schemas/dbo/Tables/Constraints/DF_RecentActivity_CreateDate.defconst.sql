ALTER TABLE [dbo].[RecentActivity]
    ADD CONSTRAINT [DF_RecentActivity_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

