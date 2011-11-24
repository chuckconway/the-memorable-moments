/*-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[Media_Retrieve17RecentPhotosByUserId]
(
	@UserId int,
	@Visibility nvarchar(50) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(17) *
	From MediaView 
	Where UserId = @UserId AND MediaView.Status = @Visibility
	Order by CreateDate desc

END*/