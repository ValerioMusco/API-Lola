CREATE TABLE [dbo].[Folder]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(250) NOT NULL,
	[Groupe] INT NOT NULL,
	[Year] INT NOT NULL,
	CONSTRAINT CK_Groupe_Folder CHECK ([Groupe] >= 0 OR [Groupe] <= 3),
	CONSTRAINT CK_Year CHECK ([Year] <=  YEAR(GETDATE()))
)
