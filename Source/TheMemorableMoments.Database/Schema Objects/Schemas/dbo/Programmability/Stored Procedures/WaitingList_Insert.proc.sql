-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE WaitingList_Insert 
	-- Add the parameters for the stored procedure here
(
	@EmailAddress nvarchar(150)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO WaitingList(EmailAddress)Values(@EmailAddress)
END