-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaUploadBatch_Insert]
	-- Add the parameters for the stored procedure here
(
	@MediaId int,
	@BatchId uniqueidentifier
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert Into MediaUploadBatch (MediaId, UploadBatch) Values(@MediaId, @BatchId)
END