-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateDate
	-- Add the parameters for the stored procedure here
(
	@Year int = null,
	@Month int = null,
	@Day int = null,
	@MediaId int	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @LedgerId int
	Set @LedgerId = (Select top(1) L.MediaLedgerId
	From MediaLedger L
	Where L.MediaId = @MediaId
	Order By InsertDate desc)

If @Year IS Not NULL
Begin
	Update MediaLedger
	Set MediaYear = @Year,
	MediaMonth = @Month,
	MediaDay = @Day
	Where MediaLedgerId = @LedgerId
End
END