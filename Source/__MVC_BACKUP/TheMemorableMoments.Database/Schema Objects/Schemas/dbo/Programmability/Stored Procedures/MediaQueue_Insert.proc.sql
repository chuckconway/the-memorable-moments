-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MediaQueue_Insert] 
	-- Add the parameters for the stored procedure here
(
	@MediaId int,
	@MediaBytes varbinary(max),
	@Filename nvarchar(250)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO MediaQueue(MediaId, MediaBytes, [Filename])VALUES(@MediaId, @MediaBytes, @Filename)
END
