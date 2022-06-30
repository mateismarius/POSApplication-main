CREATE PROCEDURE [dbo].[spGetInvoiceId]
	@invoiceId nvarchar,
	@supplier nvarchar
AS
	SELECT * FROM Invoice WHERE Invoice=@invoiceId AND Supplier=@supplier

