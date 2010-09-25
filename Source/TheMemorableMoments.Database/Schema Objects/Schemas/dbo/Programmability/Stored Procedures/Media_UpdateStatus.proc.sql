-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
Create PROCEDURE  [dbo].[Media_UpdateStatus]
( 
	@UserId int,
	@Status nvarchar(50),
	@MediaId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Media 
	SET 	
	Status = @Status 
	Where MediaId = @MediaId  AND UserId = @UserId


END
