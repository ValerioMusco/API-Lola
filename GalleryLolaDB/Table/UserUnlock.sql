CREATE TABLE [dbo].[UserUnlock]
(
	[UserToken] NVARCHAR(250) NOT NULL,
	[IdFolder] INT NOT NULL,
	CONSTRAINT FK_IdFolder_Unlock FOREIGN KEY(IdFolder) REFERENCES Folder(Id),
	CONSTRAINT PK_UserUnlock PRIMARY KEY(UserToken, IdFolder)
)
