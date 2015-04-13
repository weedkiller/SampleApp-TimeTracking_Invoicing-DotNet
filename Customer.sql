CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [GivenName] VARCHAR(50) NULL, 
    [FamilyName] VARCHAR(50) NULL, 
    [PrimaryPhone] VARCHAR(50) NULL, 
    [PrimaryEmailAddr] VARCHAR(50) NULL
)
