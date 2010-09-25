﻿ALTER TABLE [dbo].[Location]
    ADD CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationName] ASC, [UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


