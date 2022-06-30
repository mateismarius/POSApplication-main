CREATE PROCEDURE [dbo].[spUpdateProduct]
	(                                     @id            INTEGER,   
                                          @name          NVARCHAR(50),  
                                          @barcode       NCHAR(13),  
                                          @unit          NVARCHAR(10),
                                          @retailPrice   MONEY,
                                          @tax           FLOAT,
                                          @category      NVARCHAR(50),
                                          @tag1          NVARCHAR(50),
                                          @tag2          NVARCHAR(50),
                                          @expiryDate    DATE
                                          )
AS
BEGIN TRY
	UPDATE Product SET Name=@name, 
                       Barcode=@barcode,
                       Unit=@unit,
                       RetailPrice=@retailPrice,
                       Tax=@tax,
                       Category=@category,
                       Tag1=@tag1,
                       Tag2=@tag2,
                       ExpiryDate=@expiryDate
    where Id=@id
END TRY

    BEGIN CATCH
	 

    END CATCH
