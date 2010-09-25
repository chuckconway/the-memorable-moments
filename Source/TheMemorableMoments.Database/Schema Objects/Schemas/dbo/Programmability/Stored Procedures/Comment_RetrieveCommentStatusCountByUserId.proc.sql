-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_RetrieveCommentStatusCountByUserId]
( 
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--declare @UserId int
	--set @UserId = 9
	
	DECLARE @CommentStatus TABLE
	(
	  CommentStatus nvarchar(50),
	  MediaCount int
	)
	
	--Approved
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Approved' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Approved'
	
	--Pending
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Pending' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Pending'
	
	--Deleted
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Deleted' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Deleted'
	
	--Spam
	Insert Into @CommentStatus(CommentStatus, MediaCount)
	Select 'Spam' as CommentStatus, COUNT(*) as MediaCount From Comment Where UserId = @UserId AND CommentStatus = 'Spam'

	--Select CommentId, Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text],  CommentDate, UserId, ParentId, MediaId 
	--From Comment 
	--Where CommentId = @CommentId 
	
	Select * from @CommentStatus
	
	

END
