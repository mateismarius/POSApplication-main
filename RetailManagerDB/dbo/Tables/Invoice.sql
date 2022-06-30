CREATE TABLE [dbo].[Invoice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Invoice] NVARCHAR(50) NOT NULL, 
    [Supplier] NVARCHAR(50) NOT NULL, 
    [DateIn] DATE NOT NULL , 
    [Subtotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL DEFAULT 0, 
    [Total] MONEY NOT NULL
    
)
