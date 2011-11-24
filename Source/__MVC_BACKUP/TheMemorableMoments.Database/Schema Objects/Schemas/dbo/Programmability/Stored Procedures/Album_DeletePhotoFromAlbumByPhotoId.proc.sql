-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Album_DeletePhotoFromAlbumByPhotoId]
	-- Add the parameters for the stored procedure here
(
	@AlbumId int,
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Update Album
	Set CoverMediaId = Null
	Where AlbumId = @AlbumId AND
	CoverMediaId = @MediaId

    -- Insert statements for procedure here
	Delete From AlbumMedia
	Where AlbumId = @AlbumId AND
		  MediaId = @MediaId
END