CREATE TABLE [dbo].[FolderPassword]
(
	[IdFolder] INT NOT NULL,
	[Password] NVARCHAR(250) NOT NULL,
	CONSTRAINT PK_FolderPassword PRIMARY KEY([IdFolder], [Password])
)
