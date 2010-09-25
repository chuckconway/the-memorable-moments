-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Media_RetrieveByTagNameAndUserId]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@TagName nvarchar(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   * 
	From MediaView INNER JOIN
                      dbo.TagMedia ON MediaView.MediaId = dbo.TagMedia.MediaId INNER JOIN
                      dbo.Tag ON dbo.TagMedia.TagId = dbo.Tag.TagId
    Where TagName = @TagName AND MediaView.UserId = @UserId
    Order by TagName asc
END
