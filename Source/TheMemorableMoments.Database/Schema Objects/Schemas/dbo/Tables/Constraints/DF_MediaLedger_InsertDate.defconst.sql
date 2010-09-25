ALTER TABLE [dbo].[MediaLedger]
    ADD CONSTRAINT [DF_MediaLedger_InsertDate] DEFAULT (getutcdate()) FOR [InsertDate];

