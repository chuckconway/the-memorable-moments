/*-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetAllPhotosFromSite
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select FilePath, [Username] 
	From [File] F Inner Join ( Select * From MediaFile Where MediaType = 'Original') MF 
	On F.FileId = MF.FileId Inner Join Media M 
	On MF.MediaId = M.MediaId Inner Join [User] U
	ON M.UserId = U.UserId
END*/