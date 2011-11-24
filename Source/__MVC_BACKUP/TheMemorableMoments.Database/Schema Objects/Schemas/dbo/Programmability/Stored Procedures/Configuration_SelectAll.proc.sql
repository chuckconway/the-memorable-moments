-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  Configuration_SelectAll

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select ConfigurationId, SiteName, SiteTagLine, Membership, ThumbNailHeight, ThumbNailWidth, WebHeight, WebWidth 
	From Configuration

END
