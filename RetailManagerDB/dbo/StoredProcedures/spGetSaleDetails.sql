CREATE PROCEDURE [dbo].[spGetSaleDetails]
	@saleId int
AS

BEGIN TRY
BEGIN TRANSACTION
	SELECT 
		   SaleDetail.Id,
		   Product.Name AS ProductName,
		   SaleDetail.Quantity,
		   SaleDetail.RetailPrice,
		   SaleDetail.Tax,
		   SaleDetail.Total

		   from SaleDetail

		   left join Product on SaleDetail.ProductId = Product.Id
		   left join Sale on SaleDetail.SaleId = Sale.Id

		   where SaleDetail.SaleId = @saleId 
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
