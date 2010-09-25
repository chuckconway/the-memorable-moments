-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveByAlbumIdAndUserId]
( 
	@AlbumId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView Inner Join AlbumMedia ON
	MediaView.MediaId = AlbumMedia.MediaId Inner Join Album ON
	Album.AlbumId = AlbumMedia.AlbumId	
	Where Album.AlbumId = @AlbumId AND MediaView.UserId = @UserId
	Order By CASE WHEN AlbumMedia.Position IS NULL THEN 1 ELSE 0 END, AlbumMedia.Position
	--Order By AlbumMedia.Position asc

END
