﻿ALTER TABLE [dbo].[Viewed]
    ADD CONSTRAINT [PK_Viewed] PRIMARY KEY CLUSTERED ([UserId] ASC, [MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
