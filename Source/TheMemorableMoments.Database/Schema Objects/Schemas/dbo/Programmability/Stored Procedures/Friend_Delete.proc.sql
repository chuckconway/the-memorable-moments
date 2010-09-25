-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 28, 2009
-- =============================================
CREATE PROCEDURE  Friend_Delete
(
	@FriendId int,
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Friend
	Where FriendId = @FriendId AND UserId = @UserId

END
