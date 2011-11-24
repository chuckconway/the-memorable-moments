-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[UploaderMedia_SelectAllAndSetUploadStatusToPending]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @QueuedMediaItems TABLE
	(
		MediaId int,
		UploadStatus nvarchar(50),
		UserId int
	)
	
	Insert Into @QueuedMediaItems(MediaId, UploadStatus, UserId)
	Select MediaId, UploadStatus, UserId From Media Where UploadStatus = 'Queued'
	
	Update Media
	Set UploadStatus = 'Pending'
	Where UploadStatus = 'Queued'
	
	Select MediaId, UploadStatus, UserId From @QueuedMediaItems


END
