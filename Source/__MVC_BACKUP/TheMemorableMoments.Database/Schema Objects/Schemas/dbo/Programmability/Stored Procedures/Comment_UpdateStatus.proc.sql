-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
Create PROCEDURE  [dbo].[Comment_UpdateStatus]
( 
	@CommentId int,
	@CommentStatus nvarchar(50),
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Comment 
	SET CommentStatus = @CommentStatus
	From Comment C Inner Join Media M
	On C.MediaId = M.MediaId 
	Where C.CommentId = @CommentId AND M.UserId = @UserId 

END