-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PersistentCollection_Set]
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
    
    IF Exists(Select CollectionKey From PersistentCollection
    Where CollectionKey = @CollectionKey)
    BEGIN
    	Update PersistentCollection
		Set Value = @Value,
		Persistence = @Persistence,
		CreateDate = GETUTCDATE()
		Where CollectionKey = @CollectionKey    
    END
    ELSE
    BEGIN
		Insert into PersistentCollection(CollectionKey, Value, Persistence)
		Values (@CollectionKey, @Value, @Persistence)
    END
    
    Commit Transaction

END