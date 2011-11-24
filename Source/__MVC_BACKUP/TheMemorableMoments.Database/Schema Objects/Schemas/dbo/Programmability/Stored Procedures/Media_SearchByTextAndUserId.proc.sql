-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Media_SearchByTextAndUserId]
	-- Add the parameters for the stored procedure here
(
	@Search nvarchar(50),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * From MediaView
	Where UserId = @UserId AND (Title LIKE '%' + @Search + '%' OR  [Description] LIKE '%' + @Search + '%' OR Tags LIKE '%' + @Search + '%')
END
