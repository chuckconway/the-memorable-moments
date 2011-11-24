-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_RetrieveUserByLoginCredentials]
	-- Add the parameters for the stored procedure here
	@Username nvarchar(50),
	@Password nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * 
	From UserView
	Where Username = @Username AND [Password] = @Password
END
