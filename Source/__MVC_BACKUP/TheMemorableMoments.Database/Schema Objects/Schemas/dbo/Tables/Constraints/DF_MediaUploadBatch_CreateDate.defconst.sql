ALTER TABLE [dbo].[MediaUploadBatch]
    ADD CONSTRAINT [DF_MediaUploadBatch_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

