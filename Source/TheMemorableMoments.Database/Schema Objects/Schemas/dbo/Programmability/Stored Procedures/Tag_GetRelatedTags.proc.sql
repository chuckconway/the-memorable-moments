-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Tag_GetRelatedTags] 
	-- Add the parameters for the stored procedure here
(
	@TagId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select T.*, 0 as TagCount 
	From (
		Select TM.TagId 
		From 
			(
				Select *
				From TagMedia
				Where TagId = @TagId
			) M
		Inner Join TagMedia TM
			ON M.MediaId = TM.MediaId
		Where TM.TagId <> @TagId
		Group by TM.TagId
	  ) R
	Inner Join Tag T
	ON R.TagId = T.TagId
END