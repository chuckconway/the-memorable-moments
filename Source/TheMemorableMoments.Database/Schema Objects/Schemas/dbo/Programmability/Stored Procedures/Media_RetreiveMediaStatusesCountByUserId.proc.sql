-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetreiveMediaStatusesCountByUserId]
(
	@UserID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select StatusCount, Status From
		(
			Select COUNT(Status) as StatusCount, Status, UserId
			From MediaView 
			GRoup By Status, UserId
		) T
	Where T.UserId = @UserID
	
	--Union ALL
	
	--Select COUNT(*)as StatusCount,  null as Status
	--From MediaView
	--Where UserId = @UserID
	
	--Union All
	
	--Select COUNT(*)as StatusCount, 'Untagged' as Status
	--From MediaView
	--Where [Status] = 'UnPublished' AND LEN(Tags) = 0 AND UserId = @UserID
	
END
