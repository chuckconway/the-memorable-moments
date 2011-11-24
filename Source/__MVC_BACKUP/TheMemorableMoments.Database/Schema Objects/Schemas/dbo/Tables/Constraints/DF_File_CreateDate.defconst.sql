ALTER TABLE [dbo].[File]
    ADD CONSTRAINT [DF_File_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

