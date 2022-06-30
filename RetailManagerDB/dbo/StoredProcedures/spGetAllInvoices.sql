CREATE PROCEDURE [dbo].[spGetAllInvoices]
	
AS
BEGIN TRY
	set nocount on;
	DECLARE @RequestStatus INTEGER	
	SELECT Id, Invoice, Supplier, DateIn, Subtotal, Tax, Total
	FROM [dbo].[Invoice] ORDER BY Id DESC

END TRY


BEGIN CATCH
    
 IF (XACT_STATE()) = -1
      ROLLBACK TRANSACTION
 
-- Transaction committable
    IF (XACT_STATE()) = 1
      COMMIT TRANSACTION
	
   SELECT ERROR_MESSAGE() AS ErrorMessage;

  END CATCH
