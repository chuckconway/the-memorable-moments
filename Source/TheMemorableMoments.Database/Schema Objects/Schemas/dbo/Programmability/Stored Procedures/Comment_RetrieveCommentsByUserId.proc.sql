-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveCommentsByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text], CommentDate, Media.UserId, ParentId, Media.MediaId 
	From Comment Inner Join Media On
	Comment.MediaId = Media.MediaId
	Where Media.UserId = @UserId AND CommentStatus <> 'Deleted'

END
