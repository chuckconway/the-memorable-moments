-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrievePagingCountByUserIdAndLetter]
(
	@Letter char,
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
  --Declare @Letter char
  --Declare @UserId int
  --Declare @PageSize int
  --Declare @CurrentPage int
  
  --set @Letter = 'J'
  --set @UserId = 9
  --set @PageSize = 5
  --set @CurrentPage = 1

    -- Insert statements for procedure here
		Declare @Paging Table  (PageIndex VarChar(3), PageText VarChar(3))		
		Insert Into @Paging( PageIndex, PageText)Values(@Letter,@Letter)
		
		--Minus 1 from the current page. There is no page 0, the code however is based on a zero based index.
		--Declare @Head int = @PageSize * (@CurrentPage - 1)
		--Declare @Tail int = @Head + @PageSize

		Select Count(PageText) as TagCount From
		( Select ROW_NUMBER() Over (Order By TagName) AS rownum, TagId, TagName, PageText, 0 as TagCount
			From (
		   Select TagId, TagName, PageText
				From (
						Select TagMedia.TagId, TagName, [Description], TagMedia.MediaId
						From Tag inner join TagMedia 
						ON Tag.TagId = TagMedia.TagId 
						Inner Join Media M
						On M.MediaId = TagMedia.MediaId								
						Where M.UserId = @UserId AND M.Status= 'Public'
			) as TagsAndMedia inner Join @Paging ON Upper(Left(TagName,1)) = PageText
			--Where PageText <> Null
			Group by PageText, TagName, TagId
			) as FilteredTags) RowFilter
			group by PageText
	END