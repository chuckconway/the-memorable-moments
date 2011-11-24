-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[File_Update]
( 
	@FileId int,
	@FilePath nvarchar(255),
	@FileExtension nvarchar(10),
	@OriginalFileName nvarchar(200),
	@Size bigint
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update [File]
	SET
	FilePath = @FilePath,
	FileExtension = @FileExtension,
	OriginalFileName = @OriginalFileName,
	Size = @Size
 
	Where FileId = @FileId 

END
