-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UploadBatch_Insert]
	-- Add the parameters for the stored procedure here
(
	@LocationName nvarchar(100) = null,
	@Latitude decimal(18, 12) = null,
	@Longitude decimal(18, 12) = null,
	@Zoom int = null,
	@MapTypeId nvarchar(50) = null,
	@BatchId uniqueidentifier,
	@Year int = null,
	@Month int = null,
	@Day int = null,
	@Albums nvarchar(150) = null,
	@Tags nvarchar(500) = null,
	@MediaStatus nvarchar(50) = null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into UploadBatch (LocationName, Latitude, Longitude, Zoom, 
	MapTypeId, BatchId, [Year], [Month], [Day], Albums, Tags, MediaStatus)
	Values (@LocationName, @Latitude, @Longitude, @Zoom, 
	@MapTypeId, @BatchId, @Year, @Month, @Day, @Albums, @Tags, @MediaStatus)
	
	
END