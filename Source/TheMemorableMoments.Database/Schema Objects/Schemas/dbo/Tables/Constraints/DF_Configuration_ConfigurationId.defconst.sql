ALTER TABLE [dbo].[Configuration]
    ADD CONSTRAINT [DF_Configuration_ConfigurationId] DEFAULT (newid()) FOR [ConfigurationId];

