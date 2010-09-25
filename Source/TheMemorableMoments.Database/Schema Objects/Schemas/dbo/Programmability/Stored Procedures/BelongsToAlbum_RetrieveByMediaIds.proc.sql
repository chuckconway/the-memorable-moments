-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE BelongsToAlbum_RetrieveByMediaIds 
(
	@Ids IdCollection ReadOnly
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select A.AlbumId, A.Name as AlbumName, AM.MediaId 
	From AlbumMedia AM
	Inner Join AlbumView A 
		ON AM.AlbumId = A.AlbumId
	Inner Join @Ids I
		On I.Id = AM.MediaId
END