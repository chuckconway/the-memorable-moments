-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RecentActivity_RetrieveFriendsActivityFromLastWeekByUserId] 
	-- Add the parameters for the stored procedure here
(
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Select * 
	--	From RecentActivity RA Inner Join FriendView FV
	--On  RA.UserId = FV.UserId
	--Where FV.UserId = 9 AND CreateDate > DATEADD(DAY , -7, GETUTCDATE())


    -- Insert statements for procedure here
	Select ActivityType, SUM(ActivityCount) as ActivityCount , FriendId
	From RecentActivity RA Inner Join FriendView FV
	On  RA.UserId = FV.UserId
	Where FV.UserId = @UserId AND CreateDate > DATEADD(DAY , -7, GETUTCDATE())
	Group By  ActivityType, FriendId
	
END