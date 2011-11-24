-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Recent_GetYearByUserId
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

    -- Insert statements for procedure here
	Select M.*
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
	
END