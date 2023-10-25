CREATE TABLE [dbo].[Pictures]
(
	[Name] NVARCHAR(150) NOT NULL,
	[IdFolder] INT NOT NULL,
	CONSTRAINT FK_IdFolder_Pictures FOREIGN KEY(IdFolder) REFERENCES Folder(Id),
	CONSTRAINT PK_Pictures PRIMARY KEY([Name], IdFolder)
)
