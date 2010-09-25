CREATE VIEW dbo.UserView
AS
SELECT     UserId, FirstName, LastName, Password, Email, CreateDate, DisplayName, Deleted, Username, CurrentSession, LastLogin, AccountStatus, EnableReceivingOfEmails, 
                      WebViewMaxHeight, WebViewMaxWidth
FROM         dbo.[User]