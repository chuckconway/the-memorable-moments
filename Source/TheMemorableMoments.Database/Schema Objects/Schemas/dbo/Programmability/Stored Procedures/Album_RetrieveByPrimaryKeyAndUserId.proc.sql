-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveByPrimaryKeyAndUserId]
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
	From AlbumView
	Where AlbumId = @AlbumId AND UserId = @UserId

END
