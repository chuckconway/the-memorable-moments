-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_Update]
( 
	@CommentId int,
	@Name nvarchar(100),
	@Email nvarchar(255),
	@SiteUrl nvarchar(100),
	@Ip nvarchar(100),
	@UserAgent nvarchar(500),
	@CommentStatus nvarchar(50),
	@Text nvarchar(max),
	@CommentDate datetime,
	@UserId int,
	@ParentId int,
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Comment 
	SET 	
	Name = @Name,
	Email = @Email,
	SiteUrl = @SiteUrl,
	Ip = @Ip,
	UserAgent = @UserAgent,
	CommentStatus = @CommentStatus,
	[Text] = @Text,
	CommentDate = @CommentDate,
	UserId = @UserId,
	ParentId = @ParentId,
	MediaId = @MediaId
 
	Where CommentId = @CommentId 

END
