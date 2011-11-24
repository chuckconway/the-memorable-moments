/*-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Tag_UpdateOnlyDescription
	-- Add the parameters for the stored procedure here
(
	@TagId int,
	@UserId int,
	@Description nvarchar(500)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    Update Tag Set [Description] = @Description
    From Tag T inner join TagMedia TM
    ON T.TagId = TM.TagId Inner Join Media M
    ON TM.MediaId = M.MediaId
    Where T.TagId = @TagId AND M.UserId = @UserId
   
    
	
END*/