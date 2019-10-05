CREATE TABLE [dbo].[Table]
(
	[ContactId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(255) NULL, 
    [PhoneNumber] VARCHAR(50) NOT NULL, 
    [Gender] VARCHAR(50) NULL
)
