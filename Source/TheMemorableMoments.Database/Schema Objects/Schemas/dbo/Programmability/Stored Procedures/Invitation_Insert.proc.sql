-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Invitation_Insert
	-- Add the parameters for the stored procedure here
(
	@Email nvarchar(250),
	@UserId int

)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO Invitation(Email, UserId)VALUES(@Email, @UserId)	
	
	
END