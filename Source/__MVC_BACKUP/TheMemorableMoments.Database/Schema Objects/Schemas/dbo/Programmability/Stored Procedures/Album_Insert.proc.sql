-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Insert]
( 
	@Name nvarchar(50),
	@Description nvarchar(250),
	@UserId int,
	@ParentId int = null,
	@AlbumId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Album] (Name, Description, UserId, ParentId) 
	VALUES (@Name, @Description, @UserId, @ParentId)

END
