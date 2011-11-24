/*-- Batch submitted through debugger: SQLQuery13.sql|7|0|C:\Users\Chuck\AppData\Local\Temp\~vsBDD8.sql
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_SearchRetrievePhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]
( 
	@AlbumId int,
	@UserId int,
	@Search nvarchar(150) = null
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	Select * From 
	(	
		Select AM.MediaId as AlbumMediaId
		From AlbumMedia AM Inner Join Album A
		ON AM.AlbumId = A.AlbumId
		Where A.UserId = @UserId AND A.AlbumId = @AlbumId
		Group By AM.MediaId	
	) AM right Join MediaView MV
	ON AM.AlbumMediaId = MV.MediaId 
	Where AlbumMediaId is Null 
	AND (@Search is Not Null AND Len(@Search) > 0) 
	AND MV.UserId = @UserId 
	AND (Title LIKE '%' + @Search + '%' OR  [Description] LIKE '%' + @Search + '%' OR Tags LIKE '%' + @Search + '%')	
	

END*/
