﻿/*-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE DynamicCollection_Get
	-- Add the parameters for the stored procedure here
(
	@CollectionKey nvarchar(150)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CollectionKey, Value, Persistence
	From DynamicCollection
	Where CollectionKey = @CollectionKey
END*/