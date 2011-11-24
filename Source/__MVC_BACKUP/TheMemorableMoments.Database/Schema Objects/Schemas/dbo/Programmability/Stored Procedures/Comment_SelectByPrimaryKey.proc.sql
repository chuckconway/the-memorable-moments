-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_SelectByPrimaryKey]
( 
	@CommentId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text],  CommentDate, UserId, ParentId, MediaId 
	From Comment 
	Where CommentId = @CommentId 

END
