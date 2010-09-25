-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[MediaQueue_InsertExif]
(	-- Add the parameters for the stored procedure here
	@ExifCollection ExifCollection readonly
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Insert Into Exif(MediaId, [Key], Value, [Type])
	Select MediaId, [Key], Value, [Type]
	From @ExifCollection	

END