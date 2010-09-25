-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_Insert]
( 
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Password nvarchar(250),
	@Email nvarchar(250),
	@DisplayName nvarchar(100),
	@Deleted bit,
	@Username nvarchar(50),
	@AccountStatus nvarchar(50) = 'Public',
	@Identity int OUTPUT
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[User] (FirstName, LastName, Password, Email, CreateDate, DisplayName, Deleted, Username, CurrentSession, AccountStatus) 
	VALUES (@FirstName, @LastName, @Password, @Email, GETUTCDATE(), @DisplayName, @Deleted, @Username, GETUTCDATE(), @AccountStatus)
	
	set @Identity = (select @@IDENTITY)
	return @Identity

END
