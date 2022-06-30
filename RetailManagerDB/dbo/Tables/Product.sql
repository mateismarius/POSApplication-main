CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Barcode] NVARCHAR(50) NULL UNIQUE,
    [Unit] NVARCHAR(10) NOT NULL,
    [RetailPrice] MONEY NOT NULL,
    [Tax] FLOAT NOT NULL DEFAULT 100,
    [Category] NVARCHAR(50) NOT NULL, 
    [Tag1] NVARCHAR(50) NULL, 
    [Tag2] NVARCHAR(50) NULL, 
    [ExpiryDate] DATE NOT NULL 
     
    
)
