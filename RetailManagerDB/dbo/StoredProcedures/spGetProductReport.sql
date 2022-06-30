CREATE PROCEDURE [dbo].[spGetProductReport]
	
AS
	SELECT [dbo].[Product].Name, [dbo].[Product].Barcode, [dbo].[Product].Unit, [dbo].[Product].RetailPrice, [dbo].[Product].Tax, [dbo].[Product].ExpiryDate, [dbo].[Stock].Stockin, [dbo].[Stock].Stockout
	FROM [dbo].[Product]
	LEFT JOIN Stock ON [dbo].[Stock].ProductId = [dbo].[Product].Id
	ORDER BY Name
RETURN 0
