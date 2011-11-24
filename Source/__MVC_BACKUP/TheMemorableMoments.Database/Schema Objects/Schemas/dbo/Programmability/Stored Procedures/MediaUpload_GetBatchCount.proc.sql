-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE MediaUpload_GetBatchCount
	-- Add the parameters for the stored procedure here
(
	@BatchId uniqueidentifier
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select Count(*) as PhotoCount 
	From MediaUploadBatch U
	Where  U.UploadBatch = @BatchId

END