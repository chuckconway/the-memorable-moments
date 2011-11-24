-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Friend_SearchByText]
	-- Add the parameters for the stored procedure here
(
	@Search nvarchar(250),
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--declare @Search nvarchar(250)
	--declare @UserId int
   
 --  Set @Search = 'c'
 --  Set @UserId = 10
	
	-- Insert statements for procedure here
	SELECT     U.FirstName, U.LastName, U.Email, U.DisplayName, U.Username, U.UserId as FriendId,
							 (SELECT   COUNT(Media.MediaId)
								FROM          Media
								WHERE      UserId = U.UserId) AS PhotoCount,
							 COALESCE((SELECT  TOP (1) ROW_NUMBER() OVER (ORDER BY TagId ASC) AS C 
	FROM         (SELECT     TagMedia.MediaId, TM.TagId
						   FROM          (SELECT     TagId
												   FROM          TagMedia
												   GROUP BY TagId) TM INNER JOIN
												  TagMedia ON TM.TagId = TagMedia.TagId INNER JOIN
												  Media ON TagMedia.MediaId = Media.MediaId
						   WHERE      UserId = U.UserId) E
	GROUP BY TagId
	ORDER BY C DESC), 0) AS TagCount,
		(SELECT     COUNT(*) AS C
		  FROM          Friend
		  WHERE      UserId = U.UserId) AS FriendCount
	FROM [User] U left join Friend F On
	U.UserId = F.FriendId 
	Where U.UserId <> @UserId AND U.UserId Not in (Select FriendId as UserId From Friend Where UserId = @UserId) AND 
					([FirstName] LIKE '%' + @Search + '%' 
					OR [LastName] LIKE '%' + @Search + '%' 
					OR Email LIKE '%' + @Search + '%' 
					OR DisplayName LIKE '%' + @Search + '%'
					OR Username LIKE '%' + @Search + '%')				
			


END
