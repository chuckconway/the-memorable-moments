-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Insert]
( 
	@Title nvarchar(100),
	@Description nvarchar(550),
	--@TimesViewed int,
	@MediaYear int = null,
	@MediaMonth int = null,
	@MediaDay int = null,
	@UserId int,
	@Status nvarchar(50),
	@Tags nvarchar(250),
	@Identity int output
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Media] (UserId, Status) 
	VALUES (@UserId, @Status)
	
	Set @Identity =(Select @@IDENTITY)
	
	Insert INTO MediaLedger(Title, Description, Tags, MediaDay, MediaYear, MediaMonth, MediaId, UserId)
	VALUES(@Title, @Description, @Tags, @MediaDay, @MediaYear, @MediaMonth, @Identity, @UserId)
	
	return @Identity	

END
