ALTER TABLE [dbo].[AlbumMedia]
    ADD CONSTRAINT [DF_AlbumMedia_CreateDate] DEFAULT (getutcdate()) FOR [CreateDate];

