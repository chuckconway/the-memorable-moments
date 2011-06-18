-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_Delete]
( 
	@MediaId int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
BEGIN TRY
	Begin Transaction
	
	CREATE TABLE #tempFile
	(
	   FileId INT
	)
	
	Insert Into #tempFile(FileId)
	Select F.FileId 
	From [File] F
	Inner Join MediaFile MF
		On F.FileId = MF.FileId
	Where MF.MediaId = @MediaId
	
	Delete From MediaFile
	Where MediaId = @MediaId
	
	Delete F
	From [File] F
	Inner Join #tempFile MF
		On F.FileId = MF.FileId 	
	
	Delete From MediaFile
	Where MediaId = @MediaId	
	
	
	Delete From TagMedia
	Where MediaId = @MediaId
	
	Delete From Comment
	Where MediaId = @MediaId
	
	Delete From AlbumMedia
	Where MediaId = @MediaId
	
	Delete From Media 
	Where MediaId = @MediaId AND UserId = @UserId
	
	drop table #tempFile
		
	Commit
	
	END TRY
BEGIN CATCH
	Rollback
	DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    -- Use RAISERROR inside the CATCH block to return error
    -- information about the original error that caused
    -- execution to jump to the CATCH block.
    RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );
END CATCH

END
