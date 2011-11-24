-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Update]
( 
	@Title nvarchar(100),
	@Description nvarchar(550),
	@MediaYear int = null,
	@MediaMonth int = null,
	@MediaDay int = null,
	@UserId int,
	@Status nvarchar(50),
	@Tags nvarchar(250),
	@MediaId int,
	@LocationName nvarchar(100)= null,
	@Latitude decimal(18, 12) = null,
	@Longitude decimal(18, 12)= null,
	@Zoom int = null,
	@MapTypeId nvarchar(50) = null
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Media 
	SET 	
	UserId = @UserId,
	Status = @Status 
	Where MediaId = @MediaId  AND UserId = @UserId
	
	IF COALESCE(@LocationName, '') <> ''  AND
	COALESCE(cast(@Latitude as nvarchar), '') <> '' AND
	COALESCE(cast(@Longitude as nvarchar), '') <> '' AND
	COALESCE(cast(@Zoom as nvarchar), '') <> '' AND
	COALESCE(cast(@MapTypeId as nvarchar), '') <> ''
	BEGIN
	
		IF EXISTS(Select * From Location Where UserId = @UserId And LocationName = @LocationName)
			BEGIN
				Update Location
				Set Latitude = @Latitude,
				Longitude = @Longitude,
				Zoom = @Zoom,
				MapTypeId = @MapTypeId
				Where LocationName = @LocationName AND UserId = @UserId			
			END
		ELSE
		BEGIN
			Insert INTO Location(LocationName, Latitude, Longitude, Zoom, MapTypeId, UserId )
			VALUES(@LocationName, @Latitude, @Longitude, @Zoom, @MapTypeId, @UserId )
		END	
	END
	
	Insert Into MediaLedger(Title, Description, Tags, MediaYear, MediaMonth, MediaDay, MediaId, UserId, LocationName)
	Values (@Title, @Description, @Tags, @MediaYear, @MediaMonth, @MediaDay, @MediaId, @UserId, @LocationName)

END
