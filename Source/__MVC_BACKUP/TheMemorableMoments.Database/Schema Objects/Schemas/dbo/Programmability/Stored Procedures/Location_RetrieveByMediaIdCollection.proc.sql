-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Location_RetrieveByMediaIdCollection] 
	-- Add the parameters for the stored procedure here
(
	@Ids IdCollection ReadOnly	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    Select L.LocationName, L.Latitude, L.Longitude, L.Zoom, L.MapTypeId, L.UserId, M.MediaId 
    From Location L
    Inner Join 
    (    
		SELECT MediaId, Title, Description, Tags, MediaDay, MediaYear, MediaMonth, LocationName
		FROM 
		(
			SELECT MediaId, InsertDate, Title, Description, Tags, MediaDay, MediaYear, LocationName, MediaMonth,  row_number() 
			OVER (
					partition BY MEdiaID
                    ORDER BY InsertDate DESC
                 ) AS rn
			FROM dbo.MediaLedger
	    ) AS T
		WHERE rn = 1
	) M
	On L.LocationName = M.LocationName
	Inner Join @Ids I
	On I.Id = M.MediaId
                      
END