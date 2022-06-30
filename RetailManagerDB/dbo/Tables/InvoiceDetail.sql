CREATE TABLE [dbo].[InvoiceDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvoiceId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quantity] FLOAT NOT NULL, 
    [PurchasePrice] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL DEFAULT 0, 
    [TotalPrice] MONEY NOT NULL, 
    CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoice]([Id]), 
    CONSTRAINT [FK_InvoiceDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
