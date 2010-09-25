-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecentActivity_RetrieveOneMonthActivityRollUp]
(
	@UserId int
)
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
	
	----Photos added
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'PhotosAdded' as ActivityType, COUNT(*) as ActivityCount From Media Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE() ) 
	
	----Friends
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'FriendsAdded' as ActivityType, COUNT(*) as ActivityCount From Friend Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE() ) 
	
	----Albums
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'NewAlbums' as ActivityType, COUNT(*) as ActivityCount From Album Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE() ) 
	
	----Comments
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'NewComments' as ActivityType, COUNT(*) as ActivityCount 
	--From Comment C 
	--Where UserId = @UserId AND 
	--CommentDate > DATEADD(DAY , -30, GETUTCDATE())
	
	---- Album Meida
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'AddedPhotosToAlbums' as ActivityType, COUNT(*) as ActivityCount From AlbumMedia AM inner join Album A On A.AlbumId = AM.AlbumId Where A.UserId = @UserId AND AM.CreateDate > DATEADD(DAY , -30, GETUTCDATE()) 
	
	---- Tags Added
	--Insert Into @ActivityTable(ActivityType, ActivityCount)
	--Select 'TagsAddedToPhotos' as ActivityType, COUNT(*) as ActivityCount From TagMedia TM inner join Media M On TM.MediaId = M.MediaId Where M.UserId = @UserId AND TM.CreateDate > DATEADD(DAY , -30, GETUTCDATE()) 
	
	Select ActivityType, Sum(ActivityCount) as 'ActivityCount', UserId
	From RecentActivity	
	Where UserId = @UserId AND CreateDate > DATEADD(DAY , -30, GETUTCDATE())
	Group By UserId, ActivityType 


    
END
