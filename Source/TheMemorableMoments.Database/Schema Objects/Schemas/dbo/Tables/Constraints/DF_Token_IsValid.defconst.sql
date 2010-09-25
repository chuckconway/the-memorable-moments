ALTER TABLE [dbo].[Token]
    ADD CONSTRAINT [DF_Token_IsValid] DEFAULT ((1)) FOR [IsValid];

