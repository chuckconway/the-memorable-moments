-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecentActivity_OneHourActivityRollUp]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--DECLARE @ActivityTable TABLE
	--(
	--  ActivityType nvarchar(50),
	--  ActivityCount int
	--)
	
	--Photos added
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'PhotosAdded' as ActivityType, COUNT(*) as ActivityCount, UserId From Media Where CreateDate > DATEADD(HOUR , -1, GETUTCDATE() ) Group By UserId 
	
	--Friends
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'FriendsAdded' as ActivityType, COUNT(*) as ActivityCount, UserId From Friend Where CreateDate > DATEADD(HOUR , -1, GETUTCDATE() ) Group By UserId
	
	--Albums
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'NewAlbums' as ActivityType, COUNT(*) as ActivityCount, UserId From Album Where CreateDate > DATEADD(HOUR , -1, GETUTCDATE() ) Group By UserId
	
	--Comments
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'NewComments' as ActivityType, COUNT(*) as ActivityCount, UserId 
	From Comment C 
	Where CommentDate > DATEADD(HOUR , -1, GETUTCDATE())
	Group By UserId
	
	-- Album Meida
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'AddedPhotosToAlbums' as ActivityType, COUNT(*) as ActivityCount, UserId From AlbumMedia AM inner join Album A On A.AlbumId = AM.AlbumId 
	Where  AM.CreateDate > DATEADD(HOUR , -1, GETUTCDATE()) Group By UserId
	
	-- Tags Added
	Insert Into RecentActivity(ActivityType, ActivityCount, UserId)
	Select 'TagsAddedToPhotos' as ActivityType, COUNT(*) as ActivityCount, UserId 
	From TagMedia TM inner join Media M On TM.MediaId = M.MediaId 
	Where  TM.CreateDate > DATEADD(HOUR , -1, GETUTCDATE()) Group By UserId

    
END