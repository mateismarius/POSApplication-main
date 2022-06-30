CREATE TABLE [dbo].[SaleDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SaleId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quantity] FLOAT NOT NULL,
    [RetailPrice] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL DEFAULT 0, 
    [Total] MONEY NOT NULL, 
    CONSTRAINT [FK_SaleDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [FK_SaleDetail_Sale] FOREIGN KEY ([SaleId]) REFERENCES [Sale]([Id]), 
   ) 
