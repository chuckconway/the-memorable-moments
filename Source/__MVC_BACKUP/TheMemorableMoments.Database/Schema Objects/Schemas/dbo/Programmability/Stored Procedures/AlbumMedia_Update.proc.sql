/*-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  AlbumMedia_Update
( 
	@AlbumId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update AlbumMedia 
	SET 	AlbumId = @AlbumId,
	MediaId = @MediaId
 
	Where AlbumId = @AlbumId And MediaId = @MediaId 

END*/
