-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrievePagingByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		Declare @Paging Table  (PageIndex VarChar(3), PageText VarChar(3)) 	

	Begin
			Declare @StartIndex Int, @EndIndex Int, @Incr Int  
		
			Set @StartIndex = 0 
			Set @EndIndex = 0
			Set @Incr = 0  
			Select @StartIndex = Ascii('A')  
			Select @EndIndex = Ascii('Z')  
			Select @Incr = @StartIndex  
			While (@EndIndex >= @Incr )  
			Begin  
				Insert Into @Paging  
				Select Char(@Incr), Char(@Incr)  
				Select @Incr = @Incr+1  
			End  
	End

	Select [@Paging].PageIndex, COALESCE(MediaCountInLetter, 0) as MediaCountInLetter
	From
	(
		Select PageText, COUNT(PageIndex)AS MediaCountInLetter 
		From (
						Select TagMedia.TagId, TagName, [Description], TagMedia.MediaId
						From Tag inner join TagMedia 
						ON Tag.TagId = TagMedia.TagId 
						Inner Join Media M
						On M.MediaId = TagMedia.MediaId								
						Where M.UserId = @UserId AND M.Status= 'Public'
	) as TagsAndMedia Left Join @Paging ON Upper(Left(TagName,1)) = PageText
	Group by PageText) As LetterCounts Right Join @Paging 
	On LetterCounts.PageText = [@Paging].PageIndex
	END