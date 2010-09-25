ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_WebViewMaxWidth] DEFAULT ((800)) FOR [WebViewMaxWidth];

