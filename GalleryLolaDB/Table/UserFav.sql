CREATE TABLE [dbo].[UserFav]
(
	[TokenUser] NVARCHAR(250) NOT NULL,
	[IdFolder] INT NOT NULL,
	CONSTRAINT FK_IdFolder_Fav FOREIGN KEY(IdFolder) REFERENCES Folder(Id),
	CONSTRAINT PK_UserFav PRIMARY KEY(TokenUser, IdFolder)
)
