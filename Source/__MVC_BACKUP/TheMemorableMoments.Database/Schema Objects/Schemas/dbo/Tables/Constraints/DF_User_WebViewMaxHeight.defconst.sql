ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_WebViewMaxHeight] DEFAULT ((800)) FOR [WebViewMaxHeight];

