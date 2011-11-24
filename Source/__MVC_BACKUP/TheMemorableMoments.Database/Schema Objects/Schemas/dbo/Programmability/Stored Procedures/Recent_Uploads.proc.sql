-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[Recent_Uploads]
	-- Add the parameters for the stored procedure here
(
	@photoCount int,
	@UserId int,
	@Status nvarchar(20) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	Create TABLE #TopResults 
	(
		  MediaId int,
		  CreateDate datetime
	)
	
	Create TABLE #RecentUploads 
	(
		  MediaId int,
		  Age nvarchar(100)
	)
	
    -- Insert statements for procedure here
	Declare @Sql nvarchar(200); 
	set @Sql = 'Select top(' + Cast(@photoCount as nvarchar) + ') MediaId, CreateDate 
				From MediaView
				Where UserId = ' + CAST(@UserId as nvarchar) + ' AND [Status] = ''' + @Status + '''
				Order by CreateDate desc'
	
	Insert into #TopResults (MediaId, CreateDate)
	EXEC SP_EXECUTESQL @Sql		
		
	-- Day 1
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'DayOne' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(HOUR , -24, GETUTCDATE() )
	
	-- Day 2
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'DayTwo' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(HOUR , -72, GETUTCDATE())
	AND CreateDate < DATEADD(HOUR , -24, GETUTCDATE())
	
	-- Week 1
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'WeekOne' as Age 
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -14, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -3, GETUTCDATE())

	-- Week 2
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'WeekTwo' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -21, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -14, GETUTCDATE())
	
	-- Week 3
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'WeekThree' as Age 
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -27, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -21, GETUTCDATE())
	
	-- Month 1
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthOne' as Age 
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -59, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -27, GETUTCDATE())
	
	-- Month 2
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthTwo' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -89, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -59, GETUTCDATE())
	
	-- Month 3
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthThree' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -119, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -89, GETUTCDATE())
	
	-- Month 4
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthFour' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -149, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -119, GETUTCDATE())
	
	-- Month 5
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthFive' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -179, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -149, GETUTCDATE())
	
	-- Month 6
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'MonthSix' as Age
	From #TopResults T 
	Where CreateDate >= DATEADD(DAY , -209, GETUTCDATE())
	AND CreateDate < DATEADD(DAY , -179, GETUTCDATE())
	
	-- Older
	Insert INTO #RecentUploads(MediaId, Age)
	Select MediaId, 'Older' as Age
	From #TopResults T 
	Where CreateDate < DATEADD(DAY , -209, GETUTCDATE())

	Select M.*, U.Age 
	from #RecentUploads U
	Inner Join MediaView M
		On U.MediaId = M.MediaId
	Order By M.CreateDate asc
	
		
	Drop Table #RecentUploads
	Drop Table #TopResults 
	
END