﻿ALTER TABLE [dbo].[TagMedia]
    ADD CONSTRAINT [PK_TagPhoto] PRIMARY KEY CLUSTERED ([TagId] ASC, [MediaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

