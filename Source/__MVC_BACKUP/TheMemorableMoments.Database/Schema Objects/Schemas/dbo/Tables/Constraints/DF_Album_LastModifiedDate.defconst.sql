ALTER TABLE [dbo].[Album]
    ADD CONSTRAINT [DF_Album_LastModifiedDate] DEFAULT (getutcdate()) FOR [LastModifiedDate];

