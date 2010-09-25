-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Location_Save]
(
	@LocationName nvarchar(100),
	@Latitude numeric(26, 15),
	@Longitude numeric(26, 15),
	@UserId int,
	@Zoom tinyint,
	@MapTypeId nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
		IF EXists(Select * 
			      From Location 
			      Where LocationName = @LocationName
			      AND UserId = @UserId)
		BEGIN
				Update Location
				Set LocationName = @LocationName,
					Latitude = @Latitude,
					Longitude = @Longitude,
					UserId = @UserId,
					Zoom = @Zoom,
					MapTypeId = @MapTypeId
			     Where LocationName = @LocationName
			     AND UserId = @UserId		
		END 
		ELSE
		BEGIN
		
			Insert Into Location (LocationName, Latitude, Longitude, UserId, Zoom, MapTypeId)
			Values (@LocationName, @Latitude, @Longitude, @UserId, @Zoom, @MapTypeId)
		
		END	
END