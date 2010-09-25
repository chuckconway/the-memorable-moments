-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Reporting_SiteStatistics] 
	-- Add the parameters for the stored procedure here
(
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Statistics TABLE
	(
	  Name nvarchar(50),
	  [Count] int
	)
	
	--Friends
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Friends' as Name
	From Friend
	Where UserId = @UserId
	
	--Media/Photos
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Photos' as Name
	From Media
	Where UserId = @UserId
	
	--Albums
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Albums' as Name
	From Album
	Where UserId = @UserId
	
	--Tags
	Insert INTO @Statistics([Count], Name)
	Select COUNT(*) as [Count], 'Tags' as Name
	From 
	(
		Select COUNT(*) AS TagCount From Tag T Inner join TagMedia TM
		ON T.TagId = TM.TagId Inner Join Media M
		ON TM.MediaId = M.MediaId
		Where UserId = 9
		Group By TM.TagId
	) C
	
	
	
	Select Name, [Count]
	From @Statistics
	ORder By Name
	
	 
	
END