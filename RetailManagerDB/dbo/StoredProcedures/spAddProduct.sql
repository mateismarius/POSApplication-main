CREATE PROCEDURE spAddProduct (             
                                          @name          NVARCHAR(50),  
                                          @barcode       NCHAR(13), 
                                          @unit          NVARCHAR(10),
                                          @retailPrice   MONEY,
                                          @tax           FLOAT,
                                          @category      NVARCHAR(50),
                                          @tag1          NVARCHAR(50),
                                          @tag2          NVARCHAR(50),
                                          @expiryDate    date
                                          )

AS  
 
       
            BEGIN TRY
            BEGIN TRANSACTION
            INSERT INTO dbo.Product   
                        ( 
                         Name,  
                         Barcode,
                         Unit,
                         RetailPrice,
                         Tax,
                         Category,
                         Tag1,
                         Tag2,
                         ExpiryDate
                         )  
            VALUES     (  
                         @name,  
                         @barcode,
                         @unit,
                         @retailPrice,
                         @tax,
                         @category,
                         @tag1,
                         @tag2,
                         @expiryDate )  

            DECLARE @productId int
            set @productId = (SELECT TOP 1 Id from Product order by Id desc) 

        INSERT INTO Stock (ProductId) VALUES ( @productId)
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
  END CATCH