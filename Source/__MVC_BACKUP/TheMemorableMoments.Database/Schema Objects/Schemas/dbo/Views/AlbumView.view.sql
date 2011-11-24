CREATE VIEW dbo.AlbumView
AS
SELECT     A.AlbumId, A.Name, A.Description, A.UserId, A.ParentId, A.CoverMediaId, COALESCE (C.AlbumCount, 0) AS ChildAlbumCount, COALESCE (AM.PhotoCount, 0) 
                      AS PhotoCount
FROM         dbo.Album AS A LEFT OUTER JOIN
                          (SELECT     COUNT(AlbumId) AS AlbumCount, ParentId
                            FROM          dbo.Album
                            GROUP BY ParentId) AS C ON A.AlbumId = C.ParentId LEFT OUTER JOIN
                          (SELECT     COUNT(AlbumId) AS PhotoCount, AlbumId
                            FROM          dbo.AlbumMedia
                            GROUP BY AlbumId) AS AM ON A.AlbumId = AM.AlbumId