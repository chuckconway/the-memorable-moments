-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UploaderMedia_UpdateStatus]
	-- Add the parameters for the stored procedure here
(
	@MediaId int,
	@Status nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Media 
	Set [UploadStatus] = @Status
	Where MediaId = @MediaId
	
END
