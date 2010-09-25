-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[MediaQueue_GetMediaByMediaId]
( 
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select *
	From MediaViewAll 
	Where MediaId = @MediaId 

END