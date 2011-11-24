/*-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_SearchPhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]
( 
	@AlbumId int,
	@UserId int,
	@Search nvarchar(150)
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
	
	Select * 
	From MediaView left outer Join AlbumMedia ON
	MediaView.MediaId = AlbumMedia.MediaID
	Where (@Search is Not Null AND Len(@Search) > 0) 
	AND ((AlbumId <> @AlbumId Or AlbumId Is Null 
	AND MediaView.UserId = @UserId) 
	AND (Title LIKE '%' + @Search + '%' OR  [Description] LIKE '%' + @Search + '%' OR Tags LIKE '%' + @Search + '%'))
	
	
	--Select AlbumMedia.MediaId, AlbumMedia.AlbumId From MediaView inner Join AlbumMedia On
	--		MediaView.MediaId <> AlbumMedia.MediaId
	--		Group By AlbumMedia.MediaId, AlbumMedia.AlbumId
	

END*/
