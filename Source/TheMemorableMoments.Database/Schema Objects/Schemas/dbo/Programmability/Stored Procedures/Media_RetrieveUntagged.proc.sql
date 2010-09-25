/*-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_RetrieveUntagged]
(
	@userId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaView
	Where [Status] = 'UnPublished' AND LEN(Tags) = 0 AND UserId = @userId

END*/
