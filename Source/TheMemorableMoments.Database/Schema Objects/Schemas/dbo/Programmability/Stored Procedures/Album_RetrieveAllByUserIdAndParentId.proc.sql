-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveAllByUserIdAndParentId]
(
	@UserId int, 
	@ParentId int = null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Declare @UserId int 
	--Declare @ParentId int 
	--set @UserId = 9
	--set @ParentId = 5

	Select * 
	From AlbumView
	Where UserId = @UserId AND COALESCE(ParentId, 0) = COALESCE(@ParentId, 0)   
	ORder by Name asc
	
	

END
