-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Media_RetrieveByTagIdAndUserId]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@TagId int,
	@Visiblity nvarchar(50) = 'Public'
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
    Where dbo.TagMedia.TagId = @TagId AND MediaView.UserId = @UserId AND MediaView.Status = @Visiblity
    Order by TagName asc
END