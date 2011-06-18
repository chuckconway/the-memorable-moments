-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_GetRelatedTagsByYear]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@Year int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Create Table #Year
	(
		MediaId int not null
	)

	Insert Into #Year(MediaId)	
	Select M.MediaId
	From (Select	MediaId,
			Title,
			Description,
			Tags, 
			MediaDay, 
			MediaYear, 
			MediaMonth
			
	FROM          
			(SELECT MediaId, 
					InsertDate, 
					Title, 
					Description, 
					Tags, 
					MediaDay, 
					MediaYear,
					MediaMonth, 
					row_number() OVER	(
											partition BY MEdiaID 
											ORDER BY InsertDate DESC			
										) AS rn
					FROM dbo.MediaLedger
		    ) AS T
	WHERE rn = 1) E 
	Inner Join MediaView M
		ON E.MediaId = M.MediaId
	Where E.MediaYear = @Year AND M.UserId = @UserId
	
		Select T.*, 0 as TagCount 
	From (
		Select TM.TagId 
		From #Year M
		Inner Join TagMedia TM
			ON M.MediaId = TM.MediaId
		Group by TM.TagId
	  ) R
	Inner Join Tag T
	ON R.TagId = T.TagId
	
	Drop Table #Year
END