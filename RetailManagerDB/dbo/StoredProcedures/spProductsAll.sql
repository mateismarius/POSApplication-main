CREATE PROCEDURE [dbo].[spProductsAll]

AS
BEGIN TRY
	set nocount on;

	SELECT *
	from [dbo].[Product]

END TRY

	BEGIN CATCH
	 
  END CATCH
