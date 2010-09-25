-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrieveNextPreviousMedia]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @Current int
	Declare @Next int
	Declare @Previous int

   	DECLARE @NextPrevious TABLE
	(
	  NextId int,
	  PreviousId int
	)
	
	Declare @Media Table
	(
	  MediaId int,
	  RowNumber int,
	  CreateDate datetime2
	)
    
    ;WITH MediaRow AS
	( 
		SELECT MediaId, UserId, CreateDate,
		ROW_NUMBER() OVER (ORDER BY CreateDate desc) AS RowNumber
		FROM Media 
		Where UserId = 9 And Status = 'Public'
	)

	Insert INTO @Media(MediaId,RowNumber, CreateDate)
	Select MediaId, RowNumber, CreateDate
	From MediaRow
	
	
	Set @Current = (Select RowNumber From @Media Where MediaId = @MediaId)
	Set @Next = (Select MediaId From @Media Where RowNumber = (@Current - 1)  )
	Set @Previous = (Select MediaId From @Media Where RowNumber = (@Current + 1) )
	
	Insert INTO @NextPrevious(NextId, PreviousId)VALUES(IsNull(@Next, -1), IsNull(@Previous, -1))	
	
	Select * From @NextPrevious
    
	
END