-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Reporting_Top10MostViewed
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Top 5 * 
	From MediaViewed MV Inner Join MediaView M
	ON MV.MediaId = M.MediaId
	Where M.UserId = @UserId
	Order By ViewCount DESC
END