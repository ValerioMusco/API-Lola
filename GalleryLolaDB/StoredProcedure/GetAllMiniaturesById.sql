CREATE PROCEDURE [dbo].[GetAllMiniaturesById]
	@id int
AS
	SELECT CONCAT(f.Name, '/', m.Name) as [Path]
	FROM Miniatures m
	JOIN Folder f on m.IdFolder = f.Id
	WHERE f.Id = @id
RETURN 0