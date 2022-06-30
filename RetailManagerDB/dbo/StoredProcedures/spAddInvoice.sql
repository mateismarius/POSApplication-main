CREATE PROCEDURE [dbo].[spAddInvoice](    
                                          @invoice       NVARCHAR(50),  
                                          @supplier      NVARCHAR(50),  
                                          @dateIn        DATE,  
                                          @subtotal      MONEY,  
                                          @tax           MONEY,
                                          @total         MONEY
                                          )
AS  

        BEGIN  TRY 
        BEGIN TRANSACTION
            INSERT INTO dbo.Invoice  
                        (
                        
                         Invoice,  
                         Supplier,  
                         DateIn,
                         Subtotal,
                         Tax,
                         Total
                         )  
            VALUES     (  
                         
                         @invoice,  
                         @supplier,  
                         @dateIn,
                         @subtotal,
                         @tax,
                         @total)
            SELECT @@ROWCOUNT
        COMMIT TRANSACTION
  
  END TRY
  BEGIN CATCH
    
  -- Transaction uncommittable
    IF (XACT_STATE()) = -1
      ROLLBACK TRANSACTION
 
-- Transaction committable
    IF (XACT_STATE()) = 1
      COMMIT TRANSACTION

      SELECT ERROR_MESSAGE() AS ErrorMessage;

  END CATCH