-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Tag_RetrieveTagByNameAndUserId 
	-- Add the parameters for the stored procedure here
(
	@TagName nvarchar(100),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Declare @TagName nvarchar(100)
	--Declare @UserId int
	
	--set @TagName = 'erin-meraz'
	--set @UserId = 9

    -- Insert statements for procedure here
    
    Select R.TagId, TagName, [Description], 0 as TagCount 
    From (    
			Select T.TagId
			From Tag T inner join TagMedia TM
			ON T.TagId = TM.TagId Inner Join Media M
			ON TM.MediaId = M.MediaId
			Where T.TagName = @TagName AND M.UserId = @UserId
			Group By T.TagId
		) R inner join Tag T
	ON T.TagId = R.TagId
	
END