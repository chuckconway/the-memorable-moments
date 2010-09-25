CREATE PROCEDURE [dbo].[MediaUploadBatch_RetrieveMediaByBatchId]
(
	@BatchId uniqueidentifier
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * 
	From MediaUploadBatch MB Inner join MediaViewAll MV
	ON  MB.MediaId = MV.MediaId
	Where MB.UploadBatch = @BatchId
	
END