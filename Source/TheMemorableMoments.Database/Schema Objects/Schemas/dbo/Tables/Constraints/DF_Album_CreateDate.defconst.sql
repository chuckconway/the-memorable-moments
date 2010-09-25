ALTER TABLE [dbo].[Album]
    ADD CONSTRAINT [DF_Album_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

