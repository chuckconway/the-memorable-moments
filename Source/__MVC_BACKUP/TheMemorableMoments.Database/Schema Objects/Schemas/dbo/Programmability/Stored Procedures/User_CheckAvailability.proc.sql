-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE User_CheckAvailability
	-- Add the parameters for the stored procedure here
(
	@Username nvarchar(40)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select COUNT(*) as [Count] 
	From [User]
	Where Username = @Username
	
END