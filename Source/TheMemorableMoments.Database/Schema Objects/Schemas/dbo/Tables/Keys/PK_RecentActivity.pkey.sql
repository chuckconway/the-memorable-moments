﻿ALTER TABLE [dbo].[RecentActivity]
    ADD CONSTRAINT [PK_RecentActivity] PRIMARY KEY CLUSTERED ([RecentActivityId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

