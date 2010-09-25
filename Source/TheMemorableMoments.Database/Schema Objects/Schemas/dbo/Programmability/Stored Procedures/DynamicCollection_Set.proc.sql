/*-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DynamicCollection_Set]
	-- Add the parameters for the stored procedure here
(
	@CollectionKey nvarchar(150),
	@Value nvarchar(1000),
	@Persistence nvarchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    Begin Transaction
    
    IF Exists(Select CollectionKey From DynamicCollection
    Where CollectionKey = @CollectionKey)
    BEGIN
    	Update DynamicCollection
		Set Value = @Value,
		Persistence = @Persistence,
		CreateDate = GETUTCDATE()
		Where CollectionKey = @CollectionKey    
    END
    ELSE
    BEGIN
		Insert into DynamicCollection(CollectionKey, Value, Persistence)
		Values (@CollectionKey, @Value, @Persistence)
    END
    
    Commit Transaction

END*/