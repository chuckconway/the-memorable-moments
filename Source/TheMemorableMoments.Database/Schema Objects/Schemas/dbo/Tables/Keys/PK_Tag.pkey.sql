﻿ALTER TABLE [dbo].[Tag]
    ADD CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([TagId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
