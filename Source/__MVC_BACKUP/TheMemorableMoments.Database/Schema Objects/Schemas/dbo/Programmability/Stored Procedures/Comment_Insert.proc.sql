-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Sunday, November 15, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Comment_Insert]
( 
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
	@MediaId int,
	@Identity int output
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Comment] (Name, Email, SiteUrl, Ip, UserAgent, CommentStatus, [Text],  CommentDate, UserId, ParentId, MediaId) 
	VALUES (@Name, @Email, @SiteUrl, @Ip, @UserAgent, @CommentStatus, @Text, @CommentDate, @UserId, @ParentId, @MediaId)
	
	Set @Identity =(Select @@IDENTITY)
	return @Identity	

END
