CREATE PROCEDURE [dbo].[spGetAllSales]
	
AS
BEGIN TRY
	set nocount on;
	SELECT Id, LastName = (SELECT LastName FROM [dbo].[User] WHERE Id = CashierId ), SaleDate, Total
	FROM [dbo].[Sale] ORDER BY Id DESC

END TRY


BEGIN CATCH
    
 IF (XACT_STATE()) = -1
      ROLLBACK TRANSACTION
 
-- Transaction committable
    IF (XACT_STATE()) = 1
      COMMIT TRANSACTION
	
   SELECT ERROR_MESSAGE() AS ErrorMessage;

  END CATCH
