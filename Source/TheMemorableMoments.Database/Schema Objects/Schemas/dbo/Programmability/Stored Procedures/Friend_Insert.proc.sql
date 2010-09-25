-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 28, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Friend_Insert]
( 
	@FriendId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Friend] (FriendId, UserId, CreateDate) 
	VALUES (@FriendId, @UserId, GETUTCDATE())

END
