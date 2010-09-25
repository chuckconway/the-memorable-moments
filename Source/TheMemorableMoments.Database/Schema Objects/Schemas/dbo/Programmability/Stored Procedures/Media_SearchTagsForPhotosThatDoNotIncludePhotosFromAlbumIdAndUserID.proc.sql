-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Friday, October 30, 2009
-- =============================================
CREATE PROCEDURE  [dbo].[Media_SearchTagsForPhotosThatDoNotIncludePhotosFromAlbumIdAndUserID]
( 
	@AlbumId int,
	@UserId int,
	@Tags  Tags READONLY
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	--Select * From 
	--(	
	--	Select AM.MediaId as AlbumMediaId
	--	From AlbumMedia AM Inner Join Album A
	--	ON AM.AlbumId = A.AlbumId
	--	Where A.UserId = @UserId AND A.AlbumId = @AlbumId
	--	Group By AM.MediaId	
	--) AM right Join MediaView MV
	--ON AM.AlbumMediaId = MV.MediaId 
	--Where AlbumMediaId is Null 
	--AND MV.UserId = @UserId 
	--AND ( Tags LIKE '%' + @Tag + '%')
	
	Select *
	From TagMedia TM
	Inner Join (
				Select TagId
				From Tag
				Inner Join @Tags NT
					On Tag.TagName = NT.Tag
	
				) T
		On TM.TagId = T.TagId
	Inner Join MediaView M
		ON TM.MediaId = M.MediaId
	Where M.UserId = @UserId
	AND M.MediaId Not IN
	(
		Select MediaId
		From AlbumMedia A
		Where A.AlbumId = @AlbumId
	)
	
	--Select  TM. 
	--From Tag 
	--Inner Join 
	--	(Select TagId, UserId, COUNT(TagId) as TagCount 
	--	 From TagMedia Inner Join Media On 
	--			TagMedia.MediaId = Media.MediaId
	--			Group By TagId, UserId
	--	) TM ON		
	--Tag.TagId = TM.TagId
	--Inner Join (
	--			 Select TagName, TagId 
	--			 From Tag T
	--			 Inner Join @Tags NT
	--				ON T.TagName = NT.Tag				 		
	--			) T
	--ON TM.TagId = T.TagId			 
	--Where UserId = @UserId 
	--Order by T.TagName asc
	

END
