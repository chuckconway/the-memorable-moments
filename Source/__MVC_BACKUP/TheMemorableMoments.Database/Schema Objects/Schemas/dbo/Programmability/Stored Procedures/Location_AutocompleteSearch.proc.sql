-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Location_AutocompleteSearch
	-- Add the parameters for the stored procedure here
(
	@Text nvarchar(100),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select Top(10) *
    From Location
    Where UserId = @UserId 
    AND LocationName Like '%' + @Text + '%'
	
END