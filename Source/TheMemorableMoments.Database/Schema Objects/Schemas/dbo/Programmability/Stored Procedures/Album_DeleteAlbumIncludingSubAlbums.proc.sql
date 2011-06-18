-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Saturday, November 14, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Album_DeleteAlbumIncludingSubAlbums]
(
	@UserId int,
	@AlbumId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Declare	@UserId int
 -- Declare @AlbumId int

 -- Set	@UserId = 9
 -- Set	@AlbumId = 5
 
 
Create Table #tempAlbum
(
	AlbumId int,
	HLevel int	
)

;WITH Hierarchy(AlbumID, Name, ParentID, HLevel)
AS
(
    SELECT AlbumID, Name, ParentID, 0 as HLevel
    FROM Album 
    WHERE AlbumID = @AlbumId

    UNION ALL

    SELECT SubAlbum.AlbumID, SubAlbum.Name, SubAlbum.ParentID, HLevel + 1
    FROM Album SubAlbum
    INNER JOIN Hierarchy ParentDepartment ON 
    SubAlbum.ParentId = ParentDepartment.AlbumID 
)


Insert Into #tempAlbum(AlbumId, HLevel)
Select A.AlbumId, HLevel
FROM  Hierarchy Inner Join Album A ON
Hierarchy.AlbumID = A.AlbumId
Where A.UserId = @UserId
ORDER BY HLevel DESC

--Select * FROM Album A
--Inner Join #tempAlbum T
--	ON T.AlbumId = A.AlbumId

Delete AM
From AlbumMedia AM
Inner Join #tempAlbum T
	ON T.AlbumId = AM.AlbumId

Delete A
FROM Album A
Inner Join #tempAlbum T
	ON T.AlbumId = A.AlbumId

Drop table #tempAlbum

END