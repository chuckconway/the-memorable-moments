﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Recent_GetYearsForUser]
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select E.*, M.CreateDate, M.Status, M.UploadStatus, M.UserId
Into #MediaWithYear 
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
	Inner Join Media M
		ON E.MediaId = M.MediaId
	Where E.MediaYear is Not Null and M.UserId = @UserId
	
	Select MediaYear, COUNT(MediaYear) as count
	From #MediaWithYear
	group by MediaYear
	Order By MediaYear
	
	Drop table #MediaWithYear
END