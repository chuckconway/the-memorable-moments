-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertTagWithMediaId 
(
	@TagName nvarchar(100),
	@mediaId int
)
AS
BEGIN
Declare @TagId int
if EXISTS(SELECT TG.TagId, TG.TagName, TG.MediaId
	FROM  (
           Select dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId From dbo.Tag INNER JOIN
					dbo.TagMedia ON dbo.Tag.TagId = dbo.TagMedia.TagId
					GROUP BY dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId
					
				)TG
			WHERE  TG.TagName = @TagName AND MediaId = @MediaId
		)
	Begin
		 Set @tagId = (
						SELECT TG.TagId
						FROM	(
									Select dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId From  dbo.Tag INNER JOIN
									dbo.TagMedia ON dbo.Tag.TagId = dbo.TagMedia.TagId
									GROUP BY dbo.Tag.TagId, dbo.Tag.TagName, dbo.TagMedia.MediaId
								) TG
						WHERE  TG.TagName = @TagName AND MediaId = @MediaId
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
