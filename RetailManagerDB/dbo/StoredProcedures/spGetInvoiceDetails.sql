CREATE PROCEDURE [dbo].[spGetInvoiceDetails]
	@invoiceId int
AS

BEGIN TRY
BEGIN TRANSACTION
	SELECT 
		   Invoice.Invoice, 
		   Invoice.Supplier,
		   Product.Name,
		   InvoiceDetail.Quantity,
		   InvoiceDetail.PurchasePrice,
		   InvoiceDetail.Tax,
		   InvoiceDetail.TotalPrice,
		   Invoice.DateIn

		   from InvoiceDetail

		   left join Product on InvoiceDetail.ProductId = Product.Id
		   left join Invoice on InvoiceDetail.InvoiceId = Invoice.Id

		   where InvoiceDetail.InvoiceId = @invoiceId 
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