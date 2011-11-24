-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[User_RetrieveRandomPhotoByUserId]
(
	@UserId int
)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select Top(1) * 
	From MediaView M
	Where M.UserId = @UserId
	Order By NEWID()

	
END
