ALTER TABLE [dbo].[Media]
    ADD CONSTRAINT [DF_Media_UploadStatus] DEFAULT ('Uploading') FOR [UploadStatus];

