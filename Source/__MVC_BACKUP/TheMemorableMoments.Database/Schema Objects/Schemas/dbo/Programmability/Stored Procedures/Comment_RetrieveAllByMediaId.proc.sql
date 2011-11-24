
CREATE PROCEDURE  [dbo].[Comment_RetrieveAllByMediaId]
(
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text], CommentDate, UserId, ParentId, MediaId 
	From Comment
	Where MediaId = @MediaId

END
