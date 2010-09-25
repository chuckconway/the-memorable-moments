/*-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaQueue_RetrieveByMediaId] 
(
	@MediaId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select MediaId, MediaQueueId, MediaBytes, [Filename]
	From MediaQueue
	Where MediaId = @MediaId
END*/
