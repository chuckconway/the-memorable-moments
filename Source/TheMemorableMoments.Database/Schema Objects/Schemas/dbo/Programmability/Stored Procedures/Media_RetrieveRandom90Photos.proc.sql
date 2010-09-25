-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[Media_RetrieveRandom90Photos]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Top(90) *
	From MediaView 
	Where Status = 'public'
	Order by NEWID() asc

END