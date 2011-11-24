-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Search]
(
	@UserId int,
	@AlbumId int = null,
	@SearchText nvarchar(50) = null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	IF @AlbumId is NOT NULL
	BEGIN
		
	WITH Hierarchy(AlbumID, Name, ParentID, Description, UserId, PhotoCount, AlbumCount, HLevel)
	AS
	(
		SELECT AlbumID, Name, ParentID, Description, UserId, PhotoCount, ChildAlbumCount, 0 as HLevel
		FROM AlbumView 
		WHERE (AlbumID = @AlbumId AND UserId = @UserId)

		UNION ALL

		SELECT SubAlbum.AlbumID, SubAlbum.Name, SubAlbum.ParentID, SubAlbum.Description, SubAlbum.UserId, SubAlbum.PhotoCount, SubAlbum.ChildAlbumCount, HLevel + 1
		FROM AlbumView SubAlbum
		INNER JOIN Hierarchy ParentDepartment ON 
		SubAlbum.ParentId = ParentDepartment.AlbumID 
	)

	SELECT A.AlbumID, A.Name, A.ParentID, A.Description, A.UserId, HLevel, 0 as ChildAlbumCount,0 as PhotoCount
	FROM  Hierarchy Inner Join Album A ON
	Hierarchy.AlbumID = A.AlbumId
	Where A.Name Like '%' + @SearchText + '%' OR A.[Description] Like '%' + @SearchText + '%'

	ORDER BY A.Name DESC

	END
	ELSE
	BEGIN

	SELECT A.AlbumID, A.Name, A.ParentID, A.Description, A.UserId, ChildAlbumCount,PhotoCount
	FROM  AlbumView A
	Where A.UserId = @UserId AND A.Name Like '%' + @SearchText + '%' OR A.[Description] Like '%' + @SearchText + '%'

	END

END
