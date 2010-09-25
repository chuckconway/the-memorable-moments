CREATE VIEW dbo.MediaViewAll
AS
SELECT     dbo.Media.Status, dbo.Media.CreateDate, dbo.Media.UserId, dbo.Media.MediaId, dbo.[User].FirstName, dbo.[User].AccountStatus, dbo.[User].LastName, 
                      dbo.[User].Deleted, dbo.[User].Password, dbo.[User].Email, dbo.[User].DisplayName, dbo.[User].Username, dbo.[User].ShareMedia, 
                      dbo.[User].EnableFullsizeDownload, dbo.[User].EnableReceivingOfEmails, L.Title, L.Description, L.Tags, L.MediaDay, L.MediaYear, L.MediaMonth,
                          (SELECT     COUNT(*) AS CommentCount
                            FROM          Comment
                            WHERE      Comment.MediaId = dbo.Media.MediaId) AS CommentCount
FROM         dbo.Media INNER JOIN
                      dbo.[User] ON dbo.Media.UserId = dbo.[User].UserId INNER JOIN
                          (SELECT     MediaId, Title, Description, Tags, MediaDay, MediaYear, MediaMonth
                            FROM          (SELECT     MediaId, InsertDate, Title, Description, Tags, MediaDay, MediaYear, MediaMonth, row_number() OVER (partition BY MEdiaID
                                                    ORDER BY InsertDate DESC) AS rn
                            FROM          dbo.MediaLedger) AS T
WHERE     rn = 1) AS L ON Media.MediaId = L.MediaId