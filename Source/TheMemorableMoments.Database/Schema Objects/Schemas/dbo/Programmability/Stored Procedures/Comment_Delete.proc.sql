-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  Comment_Delete
( 
	@CommentId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Comment 
	Where CommentId = @CommentId 

END
