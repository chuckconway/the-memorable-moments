﻿ALTER TABLE [dbo].[PersistentCollection]
    ADD CONSTRAINT [PK_DynamicCollection] PRIMARY KEY CLUSTERED ([CollectionKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

