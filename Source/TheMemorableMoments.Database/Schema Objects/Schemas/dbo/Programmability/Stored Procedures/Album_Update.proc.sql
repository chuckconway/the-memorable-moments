-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Update]
( 
	@AlbumId int,
	@Name nvarchar(50),
	@Description nvarchar(250),
	@UserId int,
	@ParentId int = null
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Album 
	SET 
	Name = @Name,
	Description = @Description,
	UserId = @UserId,
	ParentId = @ParentId
 
	Where AlbumId = @AlbumId 

END
