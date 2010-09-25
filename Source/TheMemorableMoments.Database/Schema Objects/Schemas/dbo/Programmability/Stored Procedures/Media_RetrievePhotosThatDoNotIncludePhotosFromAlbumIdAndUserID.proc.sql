-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]
( 
	@AlbumId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Select  MediaView.MediaId
	--From  
	--(
	--	Select AlbumMedia.MediaId, AlbumMedia.AlbumId From
	--	(	
	--	) D Inner Join AlbumMedia ON
	--	AlbumMedia.MediaId <> D.MediaId
	--) T  Inner Join MediaView ON
	--T.MediaID = MediaView.MediaId
	
	Select * From 
	(	
		Select AM.MediaId as AlbumMediaId
		From AlbumMedia AM Inner Join Album A
		ON AM.AlbumId = A.AlbumId
		Where A.UserId = @UserId AND A.AlbumId = @AlbumId
		Group By AM.MediaId	
	) AM right Join MediaView MV
	ON AM.AlbumMediaId = MV.MediaId 
	Where AlbumMediaId is Null AND MV.UserId = @UserId
	
	
	--Select AlbumMedia.MediaId, AlbumMedia.AlbumId From MediaView inner Join AlbumMedia On
	--		MediaView.MediaId <> AlbumMedia.MediaId
	--		Group By AlbumMedia.MediaId, AlbumMedia.AlbumId
	

END
