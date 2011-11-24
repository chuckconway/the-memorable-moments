-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveCommentByStatusAndUserId]
(
	@UserId int,
	@CommentStatus nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select * 
	From CommentView
	Where CommentStatus = @CommentStatus
	And MediaUserId = @UserId

END
