-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[File_Insert]
( 
	@FilePath nvarchar(255),
	@FileExtension nvarchar(10),
	@OriginalFileName nvarchar(200),
	@MediaId int,
	@Identity int output,
	@MediaFormat nvarchar(50),
	@MediaType nvarchar(50), 
	@Size bigint,
	@Width int,
	@Height int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[File] (FilePath, FileExtension, OriginalFileName, CreateDate, Size, Width, Height) 
	VALUES (@FilePath, @FileExtension, @OriginalFileName, GETUTCDATE(), @Size, @Width, @Height)	

	Set @Identity =(Select @@IDENTITY)	
	Insert INTO MediaFile(MediaId, FileId, MediaFormat, MediaType) VALUES 
	(@MediaId, @Identity, @MediaFormat, @MediaType) 
	
	return @Identity

END
