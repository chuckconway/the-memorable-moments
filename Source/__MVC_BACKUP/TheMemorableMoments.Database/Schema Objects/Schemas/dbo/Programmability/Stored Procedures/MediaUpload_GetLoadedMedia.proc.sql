-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE MediaUpload_GetLoadedMedia
(
	@BatchId uniqueidentifier
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @Total int
	Set @Total = (Select Count(UB.UploadBatch) as Total From MediaUploadBatch UB where UB.UploadBatch = @BatchId)

	Select @Total as TotalUploadInBatch, MV.* 
	From MediaUploadBatch U
	Inner Join MediaView MV
		On U.MediaId = MV.MediaId
	Inner join Media M
		ON MV.MediaId = M.MediaId
	Where M.UploadStatus = 'Completed' AND U.UploadBatch = @BatchId

END