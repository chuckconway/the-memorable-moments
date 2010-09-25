-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[AlbumMedia_Insert]
( 
	@AlbumId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF NOT EXISTS(Select * FROM AlbumMedia WHERE AlbumId = @AlbumId AND MediaId = @MediaId)
	BEGIN
		INSERT INTO [dbo].[AlbumMedia] (AlbumId, MediaId) 
		VALUES (@AlbumId, @MediaId)
	END

END
