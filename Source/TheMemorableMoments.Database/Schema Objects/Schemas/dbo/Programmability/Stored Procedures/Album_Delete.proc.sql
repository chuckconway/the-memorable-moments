-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  Album_Delete
( 
	@AlbumId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Album 
	Where AlbumId = @AlbumId 

END
