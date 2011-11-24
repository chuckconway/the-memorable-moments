-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaViewed_IncrementMediaCount]
	-- Add the parameters for the stored procedure here
	@MediaId int,
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF Exists(
				Select * 
				From MediaViewed 
				Where MediaId = @MediaId AND UserId = @UserId
			  )
		BEGIN
		
			Update MediaViewed
			Set ViewCount = (ViewCount + 1),
			LastViewed = (getutcdate())
			Where MediaId = @MediaId AND UserId = @UserId
		
		END
	ELSE
		BEGIN
			Insert Into MediaViewed(MediaId, UserId) Values (@MediaId, @UserId)
		END
	
END