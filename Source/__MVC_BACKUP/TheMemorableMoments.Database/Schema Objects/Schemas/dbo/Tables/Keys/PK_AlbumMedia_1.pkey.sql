﻿ALTER TABLE [dbo].[AlbumMedia]
    ADD CONSTRAINT [PK_AlbumMedia_1] PRIMARY KEY CLUSTERED ([AlbumId] ASC, [MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

