-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveRecent5CommentsByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(5) CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text], CommentDate, M.UserId, ParentId, M.MediaId 
	From Comment C Inner Join Media M On M.MediaId = C.MediaId
	Where M.UserId = @UserId AND CommentStatus = 'Approved'
	Order by CommentDate desc

END
