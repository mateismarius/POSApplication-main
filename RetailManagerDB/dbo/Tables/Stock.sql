CREATE TABLE [dbo].[Stock]
(
    [ProductId] INT NOT NULL, 
    [Stockin] FLOAT NOT NULL DEFAULT 0,
    [Stockout] FLOAT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Stock_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product](Id),
)
