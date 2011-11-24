-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_RetrieveTagsByUserId]
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@Visibility nvarchar(50) = 'Public'
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--WITH tm (TagId, MediaId, UserId)
	--AS
	--(		

		
	--  Select * From(Select Tag.TagId From Tag Inner join TagMedia On
	--	Tag.TagId = TagMedia.TagId
	--	Group By Tag.TagId) T Inner Join On 
		
		
		
		
	-- select TagId, MediaId, UserId from(	
	--	 Select TagId, media.MediaId, UserId 
	--	 From TagMedia Inner Join  Media On
	--	 TagMedia.MediaId = Media.MediaId
	--	 Group By TagId, Media.MediaId, UserId)T
	--	 Group by TagId, MediaId, UserId
	--)


 --   -- Insert statements for procedure here
	--SELECT  Tag.TagId, TagName
	--FROM Tag Inner Join tm ON
	--Tag.TagId = tm.TagId 
	Select Tag.TagId, TagName, TagCount, [Description] From Tag Inner Join (Select TagId, UserId, COUNT(TagId) as TagCount From TagMedia Inner Join Media On 
	TagMedia.MediaId = Media.MediaId
	Where Media.Status = @Visibility
	Group By TagId, UserId) TM ON
	Tag.TagId = TM.TagId                
	Where UserId = @UserId
	Order by TagName asc
    
    	--Select tagId From Tag Group By TagId) T INNER JOIN
     --                 dbo.TagMedia ON T.TagId = dbo.TagMedia.TagId INNER JOIN
     --                 dbo.Media ON dbo.TagMedia.MediaId = dbo.Media.MediaId
     --      Group By dbo.TagMedia.TagId, T.TagId, dbo.Media.UserId ) G  
END
