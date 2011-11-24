-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Album_UpdateMediaPosition]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@Position int,
	@MediaId int,
	@AlbumId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update AlbumMedia Set Position = @Position
	From AlbumMedia inner join 
	(
		Select AlbumId 
		From Album 
		Where Album.AlbumId = @AlbumId AND Album.UserId = @UserId
	) A On
	AlbumMedia.AlbumId = A.AlbumId
	Where AlbumMedia.MediaId = @MediaId
END