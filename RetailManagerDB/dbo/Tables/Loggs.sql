CREATE TABLE [dbo].[Loggs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [LoggedIn] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [LoggedOut] DATETIME2 NULL , 
    CONSTRAINT [FK_Loggs_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
