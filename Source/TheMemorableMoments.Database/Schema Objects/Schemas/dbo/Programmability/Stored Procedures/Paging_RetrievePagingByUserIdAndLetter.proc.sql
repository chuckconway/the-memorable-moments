-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Paging_RetrievePagingByUserIdAndLetter]
(
	@Letter char,
	@UserId int,
	@PageSize int,
	@CurrentPage int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

    -- Insert statements for procedure here
		Declare @Paging Table  (PageIndex VarChar(3), PageText VarChar(3))		
		Insert Into @Paging( PageIndex, PageText)Values(@Letter,@Letter)
		
		--Minus 1 from the current page. There is no page 0, the code however is based on a zero based index.
		Declare @Head int = @PageSize * (@CurrentPage - 1)
		Declare @Tail int = @Head + @PageSize

		Select * From
		( Select ROW_NUMBER() Over (Order By TagName) AS rownum, TagId, TagName, [Description], PageText, 0 as TagCount
			From (
		   Select TagId, TagName, PageText, [Description]
				From (
						Select TagMedia.TagId, TagName, [Description], TagMedia.MediaId
						From Tag inner join TagMedia 
						ON Tag.TagId = TagMedia.TagId 
						Inner Join Media M
						On M.MediaId = TagMedia.MediaId								
						Where M.UserId = @UserId AND M.Status= 'Public'
								
		) as TagsAndMedia inner Join @Paging ON Upper(Left(TagName,1)) = PageText
			--Where PageText <> Null
			Group by PageText, TagName, TagId, [Description]
			) as FilteredTags) RowFilter
		Where RowFilter.rownum > @Head AND RowFilter.rownum <= @Tail
	END