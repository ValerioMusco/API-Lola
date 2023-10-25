CREATE TABLE [dbo].[UserAccess]
(
	[TokenUser] NVARCHAR(250) NOT NULL,
	[Groupe] INT NOT NULL,
	CONSTRAINT CK_Groupe_Access CHECK ([Groupe] >= 0 OR [Groupe] <= 3),
	CONSTRAINT PK_UserAccess PRIMARY KEY(TokenUser, Groupe)
)
