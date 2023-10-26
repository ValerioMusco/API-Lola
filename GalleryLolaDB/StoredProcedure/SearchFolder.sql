CREATE PROCEDURE [dbo].[SearchFolder]
	@querry nvarchar(150)
AS
	SELECT * FROM Folder WHERE [Name] COLLATE Latin1_General_CI_AI LIKE @querry
RETURN 0
