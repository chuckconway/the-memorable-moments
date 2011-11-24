-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_InsertTagWithMediaId] 
(
	@TagName nvarchar(100),
	@mediaId int
)
AS
BEGIN
Declare @TagId int
if EXISTS(SELECT TagName
			FROM dbo.Tag  
			WHERE  TagName = @TagName
		)
	Begin
		 Set @tagId = (
						SELECT TagId
						FROM dbo.Tag
						WHERE  TagName = @TagName
					   )
	   

		Insert Into TagMedia(TagID,MediaId) Values (@tagId, @MediaId)
	End
ELSE
	Begin
		Insert INTO Tag(TagName)Values(@TagName)
		Declare @newTagId int
		set @newTagId = (Select @@IDENTITY)

		Insert Into TagMedia(TagID,MediaId) Values (@newTagId, @MediaId)
	End
END
