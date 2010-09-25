-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_RetrieveAlbumHierarchyByAlbumIdAndUserId]
(
	@UserId int,
	@AlbumId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

WITH Hierarchy(AlbumID, Name, ParentID, HLevel)
AS
(
    SELECT AlbumID, Name, ParentID, 0 as HLevel
    FROM Album 
    WHERE AlbumID = @AlbumId

    UNION ALL

    SELECT SubAlbum.AlbumID, SubAlbum.Name, SubAlbum.ParentID, HLevel + 1
    FROM Album SubAlbum
    INNER JOIN Hierarchy ParentDepartment ON 
    SubAlbum.AlbumID = ParentDepartment.ParentID 
)

SELECT A.AlbumID, A.Name, A.ParentID, A.Description, A.UserId, HLevel, 0 as AlbumCount,0 as PhotoCount
FROM  Hierarchy Inner Join Album A ON
Hierarchy.AlbumID = A.AlbumId
Where A.UserId = @UserId

ORDER BY HLevel DESC

END
