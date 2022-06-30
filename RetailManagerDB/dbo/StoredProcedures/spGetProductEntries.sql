CREATE PROCEDURE [dbo].[spGetProductEntries]
	@productId int	
AS

BEGIN TRY
	BEGIN TRANSACTION
	SELECT 
		   Invoice.Invoice, 
		   Invoice.Supplier,
		   Product.Name,
		   Invoice.Invoice, 
		   Invoice.Supplier,
		   InvoiceDetail.Quantity,
		   InvoiceDetail.PurchasePrice,
		   InvoiceDetail.Tax,
		   InvoiceDetail.TotalPrice,
		   Invoice.DateIn

		   from InvoiceDetail

		   left join Product on InvoiceDetail.ProductId = Product.Id
		   left join Invoice on InvoiceDetail.InvoiceId = Invoice.Id

		   where InvoiceDetail.ProductId = @productId
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
