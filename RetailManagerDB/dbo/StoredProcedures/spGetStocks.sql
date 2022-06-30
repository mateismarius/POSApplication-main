CREATE PROCEDURE [dbo].[spGetStocks]
	

	AS
BEGIN TRY
	set nocount on;
	DECLARE @RequestStatus INTEGER	
	SELECT * FROM [dbo].[Stock]

END TRY


BEGIN CATCH
    
 IF (XACT_STATE()) = -1
      ROLLBACK TRANSACTION
 
-- Transaction committable
    IF (XACT_STATE()) = 1
      COMMIT TRANSACTION
	
   SELECT ERROR_MESSAGE() AS ErrorMessage;

  END CATCH
