-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[User_Update]
( 
	@UserId int,
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Password nvarchar(250),
	@Email nvarchar(250),
	@DisplayName nvarchar(100),
	@Deleted bit,
	@Username nvarchar(50),
	@AccountStatus nvarchar(50),
	@EnableReceivingOfEmails bit,
	@WebViewMaxHeight smallint , 
	@WebViewMaxWidth smallint
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update [User]
	SET 
	FirstName = @FirstName,
	LastName = @LastName,
	[Password] = @Password,
	Email = @Email,
	DisplayName = @DisplayName,
	Deleted = @Deleted,
	Username = @Username,
	AccountStatus = @AccountStatus,
	EnableReceivingOfEmails = @EnableReceivingOfEmails,
	WebViewMaxHeight = @WebViewMaxHeight, 
	WebViewMaxWidth = @WebViewMaxWidth
	
 
	Where UserId = @UserId 

END
