CREATE TABLE [dbo].[Miniatures]
(
	[Name] NVARCHAR(150) NOT NULL,
	[IdFolder] INT NOT NULL,
	CONSTRAINT FK_IdFolder_Miniatures FOREIGN KEY(IdFolder) REFERENCES Folder(Id),
	CONSTRAINT PK_Miniatures PRIMARY KEY([Name], IdFolder)
)
