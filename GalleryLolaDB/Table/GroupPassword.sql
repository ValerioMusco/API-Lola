CREATE TABLE [dbo].[GroupPassword]
(
	[Group] INT NOT NULL,
	[Password] NVARCHAR(250) NOT NULL,
	CONSTRAINT PK_GroupPassword PRIMARY KEY ([Group],[Password])
)
