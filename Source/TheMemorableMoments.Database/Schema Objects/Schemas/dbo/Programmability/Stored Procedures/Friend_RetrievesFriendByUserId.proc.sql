-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 28, 2009
-- =============================================
Create PROCEDURE  [dbo].[Friend_RetrievesFriendByUserId]
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From FriendView
	Where UserId = @UserId

END
