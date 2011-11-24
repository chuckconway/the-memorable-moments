-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[File_RetrieveByMediaIdCollection] 
	-- Add the parameters for the stored procedure here
(
	@Ids IdCollection ReadOnly
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  * FROM FileView FV
	Inner join @Ids I
		ON  I.Id = FV.MediaId --  where MediaId = @MediaId
                      
END