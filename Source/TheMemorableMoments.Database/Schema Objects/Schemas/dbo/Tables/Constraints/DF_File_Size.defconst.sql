ALTER TABLE [dbo].[File]
    ADD CONSTRAINT [DF_File_Size] DEFAULT ((0)) FOR [Size];

