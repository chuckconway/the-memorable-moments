-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_RetrieveUserAndMedia]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select * From MediaView inner join 
								(
									Select UserId, (Select Top(1) MediaId From Media Where Status = 'Public' AND UserId = M.UserId ORder By NewId()) as MediaId  From Media as M Group By UserId
								) E On MediaView.MediaId = E.MediaId 
    

	
	
END
