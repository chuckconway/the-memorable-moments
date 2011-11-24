-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Invitation_UserInvitationsRemaining 
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
    
    Declare @InvitationCount int
    Set @InvitationCount = 
    (
		Select COUNT(*) 
		From Invitation
		Where Invitation.UserId = @UserId
	)
    
    Declare @MaxInvitations int
	Set @MaxInvitations =
	(
		Select MaxInvitations 
		From dbo.[User]
		Where dbo.[User].UserId = @UserId
	)
	
	Declare @Remaining int
	Set @Remaining = (@MaxInvitations - @InvitationCount)
	
	IF (@Remaining < 0)
	BEGIN
	   set	@Remaining = 0
	END
	
	Select @Remaining as RemainingInvitations

	
END