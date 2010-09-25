-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
Create PROCEDURE  [dbo].[AlbumMedia_UpdateAlbumIdsForMedia]
( 
	@Ids IdCollection ReadOnly,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Delete From AlbumMedia
	Where MediaId = @MediaId
	
	Insert Into AlbumMedia(AlbumId, MediaId)
	Select Id as AlbumId, @MediaId as MediaId
	From @ids


END